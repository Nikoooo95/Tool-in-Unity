#include "Tool.hpp"
#include "Layer.hpp"
#include <stdlib.h>
#include "stdafx.h"

namespace tool 
{
	bool Tool::parse(const std::string & path) 
	{
		xml_document<> doc;

		//Just start to read the document as a XML
		using std::ifstream;
		ifstream xmlFile(path);
		std::vector<char> buffer((std::istreambuf_iterator<char>(xmlFile)), std::istreambuf_iterator<char>());
		buffer.push_back('\0');
		doc.parse<0>(&buffer[0]);
		xml_node<> * root_node = doc.first_node();

		if (root_node && std::string(root_node->name()) == "gonic") 
		{
			gonicFile.reset(new Gonic());
			if (!parseFile(root_node))
				return false;
		}
		return true;
	}

	bool Tool::parseFile(xml_Node * fileNode) 
	{
		std::string layerName;

		for (xml_Node * layerNode = fileNode->first_node(); layerNode; layerNode = layerNode->next_sibling()) 
		{
			if (layerNode->type() == node_element) 
			{
				//When finds the layer...
				if (std::string(layerNode->name()) == "layer") 
				{
					for (xml_Attribute * attribute = layerNode->first_attribute(); attribute; attribute = attribute->next_attribute()) 
					{
						if (std::string(attribute->name()) == "name")
							layerName = attribute->value();

						if (layerName.empty())
							return false;
					}
					std::shared_ptr<Layer2D> layer(new Layer2D(layerName));
					if (!parseLayer(layerNode, layer))
						return false;
					gonicFile->addLayer2d(layerName, layer);
				}
			}
		}
		return true;
	}

	bool Tool::parseLayer(xml_Node * layerNode, std::shared_ptr<Layer2D> layer) 
	{
		std::string nameModel;
		float height;
		for (xml_Node * modelNode = layerNode->first_node(); modelNode; modelNode = modelNode->next_sibling()) 
		{
			if (modelNode->type() == node_element) 
			{
				//When finds a model in the layer...
				if (std::string(modelNode->name()) == "model")
				{
					for (xml_Attribute * attribute = modelNode->first_attribute(); attribute; attribute = attribute->next_attribute()) 
					{
						if (std::string(attribute->name()) == "name")
							nameModel = attribute->value();

						if (std::string(attribute->name()) == "height")
							height = (float)atof(attribute->value());
					}

					if (nameModel.empty())
						return false;

					std::shared_ptr<Model2D> model(new Model2D(nameModel, height));
					if (!parseModel(modelNode, model))
						return false;

					layer->addModel(nameModel, model);
				}
			}
		}
		return true;
	}

	bool Tool::parseModel(xml_Node * modelNode, std::shared_ptr<Model2D> model) 
	{
		for (xml_Node * componentNode = modelNode->first_node(); componentNode; componentNode = componentNode->next_sibling()) {
			if (componentNode->type() == node_element) 
			{
				//When finds a vertex in the model...
				if (std::string(componentNode->name()) == "vertex")
				{
					if(!parseVertex(componentNode, model))
						return false;
				}//When finds a color in the model...
				else if (std::string(componentNode->name()) == "color")
				{
					if (!parseColor(componentNode, model))
						return false;
				}
			}
		}
		return true;
	}

	bool Tool::parseVertex(xml_Node * vertexNode, std::shared_ptr<Model2D> model) 
	{
		std::string nameNode;

		for (xml_Node * vertNode = vertexNode->first_node(); vertNode; vertNode = vertNode->next_sibling()) 
		{
			nameNode = vertNode->value();
			if (vertNode->type() == node_element) 
			{
				//Parsing of each vertex
				if (std::string(vertNode->name()) == "v") 
				{
					std::shared_ptr<Vector2> vertex(new Vector2());
					size_t nComa = std::count(nameNode.begin(), nameNode.end(), ',');
					std::vector<float> values;

					for (size_t i = 0; i < nComa + 1; ++i) 
					{
						size_t position;
						position = nameNode.find(',');
						values.push_back(std::stof(nameNode.substr(0, position)));
						nameNode.erase(0, position + 1);
					}

					vertex->set(values[0], values[1]);
					model->addVertex(vertex);
				}
			}
		}
		return true;
	}

	bool Tool::parseColor(xml_Node * colorNode, std::shared_ptr<Model2D> model) 
	{
		std::shared_ptr<Color> color(new Color());
		std::string nameNode = colorNode->value();
		size_t nComa = std::count(nameNode.begin(), nameNode.end(), ',');

		std::vector<float> values;

		for (size_t i = 0; i < nComa + 1; ++i)
		{
			size_t position;
			position = nameNode.find(',');
			values.push_back(std::stof(nameNode.substr(0, position)));
			nameNode.erase(0, position + 1);
		}

		color->set(values[0], values[1], values[2]);
		model->setColor(color);

		return true;
	}



	const char* Tool::charToString(std::string dataValue) {
		int lenStr = (int)dataValue.length() + 1;
		char* answer = new char[lenStr];
		const char * constAnswer = new char[lenStr];
		strcpy_s(answer, lenStr, dataValue.c_str());
		constAnswer = answer;
		return constAnswer;
	}

	void Tool::generateLayer3d(int layer)
	{
		std::shared_ptr<Layer2D> layer2D = gonicFile->getLayer2D(layer);
		std::shared_ptr<Layer3D> layer3D(new Layer3D(layer2D->getName()));
		gonicFile->addLayer3d(layer3D->getName(), layer3D);
	}

	void Tool::transform2dTo3d(int layer, int model) {

		std::shared_ptr<Layer3D> layer3D = gonicFile->getLayer3D(layer);

		std::shared_ptr<Model2D> model2D = gonicFile->getModelInLayer2D(layer, model);
		std::shared_ptr<Model3D> model3D(new Model3D(model2D->getName(), model2D->getHeight()));

		model3D->setColor(model2D->getColor());

		for (int i = 0; i < model2D->getVectorsAmount(); ++i)
		{
			std::shared_ptr<Vector2> v2 = model2D->getVector(i);
			std::shared_ptr<Vector3> v3(new Vector3(v2->x, 0.0f, v2->y));
			model3D->addVertex(v3);
			v3.reset(new Vector3(v2->x, model3D->getHeight(), v2->y));
			model3D->addVertex(v3);
		}
		
		layer3D->addModel(model3D->getName(), model3D);
		
	}

	void Tool::generateTriangles(int triangles[], int amount, bool backFaces, bool looped)
	{
		if (looped)
			amount -= 2;

		for (int i = 0, j = 0; i < amount; i += 6, j += 2)
		{
			triangles[i] = j; 
			triangles[i + 1] = j + 2;
			triangles[i + 2] = j + 1;

			triangles[i + 3] = j + 1;
			triangles[i + 4] = j + 2;
			triangles[i + 5] = j + 3;
			if (backFaces)
			{
				triangles[i + 6] = j + 1;
				triangles[i + 7] = j + 2;
				triangles[i + 8] = j;

				triangles[i + 9] = j + 1;
				triangles[i + 10] = j + 3;
				triangles[i + 11] = j + 2;
				i += 6;

			}
		}

		if (looped)
		{
			amount += 2;
			if (!backFaces)
			{

				triangles[amount - 6] = triangles[amount - 8];
				triangles[amount - 5] = triangles[0];
				triangles[amount - 4] = triangles[amount - 7];

				triangles[amount - 3] = triangles[amount - 7];
				triangles[amount - 2] = triangles[0];
				triangles[amount - 1] = triangles[2];
			}
			else
			{
				triangles[amount - 12] = triangles[amount - 20];
				triangles[amount - 11] = triangles[0];
				triangles[amount - 10] = triangles[amount - 19];

				triangles[amount - 9] = triangles[amount - 19];
				triangles[amount - 8] = triangles[0];
				triangles[amount - 7] = triangles[2];

				triangles[amount - 6] = triangles[amount - 9];
				triangles[amount - 5] = triangles[0];
				triangles[amount - 4] = triangles[amount - 12];

				triangles[amount - 3] = triangles[amount - 9];
				triangles[amount - 2] = triangles[2];
				triangles[amount - 1] = triangles[0];
			}
		}
	}
}