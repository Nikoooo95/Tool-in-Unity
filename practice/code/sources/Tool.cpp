#include "Tool.hpp"
#include "Layer.hpp"
#include <stdlib.h>

namespace tool {
	bool Tool::parse(const std::string & path) {
		xml_document<> doc;

		using std::ifstream;
		ifstream xmlFile(path);
		std::vector<char> buffer((std::istreambuf_iterator<char>(xmlFile)), std::istreambuf_iterator<char>());
		buffer.push_back('\0');
		doc.parse<0>(&buffer[0]);
		xml_node<> * root_node = doc.first_node();

		if (root_node && std::string(root_node->name()) == "gonic") {
			gonicFile.reset(new Gonic());
			
			if (!parseFile(root_node)) {
				return false;
			}
			
		}
		return true;
	}

	bool Tool::parseFile(xml_Node * fileNode) {
		std::string layerName;
		for (xml_Node * layerNode = fileNode->first_node(); layerNode; layerNode = layerNode->next_sibling()) {
			if (layerNode->type() == node_element) {
				if (std::string(layerNode->name()) == "layer") {

					for (xml_Attribute * attribute = layerNode->first_attribute(); attribute; attribute = attribute->next_attribute()) {
						if (std::string(attribute->name()) == "name") {
							layerName = attribute->value();
						}
						if (layerName.empty())
							return false;
					}

					std::shared_ptr<Layer> layer(new Layer(layerName));
					if (!parseLayer(layerNode, layer)) {
						return false;
					}
					gonicFile->addLayer(layerName, layer);
				}
			}
		}
		return true;
	}

	bool Tool::parseLayer(xml_Node * layerNode, std::shared_ptr<Layer> layer) {
		std::string nameModel;
		float height;
		for (xml_Node * modelNode = layerNode->first_node(); modelNode; modelNode = modelNode->next_sibling()) {
			if (modelNode->type() == node_element) {
				if (std::string(modelNode->name()) == "model") {
					for (xml_Attribute * attribute = modelNode->first_attribute(); attribute; attribute = attribute->next_attribute()) {
						if (std::string(attribute->name()) == "name") {
							nameModel = attribute->value();
							
						}
						
						if (std::string(attribute->name()) == "height") {
							height = atof(attribute->value());
						}
					}

					if (nameModel.empty()) {
						return false;
					}

					std::shared_ptr<Model2D> model(new Model2D(nameModel));
					if (!parseModel(modelNode, model)) {
						return false;
					}
					layer->addModel(nameModel, model);
				}
			}
		}

		return true;
	}

	bool Tool::parseModel(xml_Node * modelNode, std::shared_ptr<Model2D> model) {
		for (xml_Node * componentNode = modelNode->first_node(); componentNode; componentNode = componentNode->next_sibling()) {
			if (componentNode->type() == node_element) {
				if (std::string(componentNode->name()) == "vertex") {
					std::shared_ptr<Vector2> vertex(new Vector2());
					if (!parseVertex(componentNode, vertex)) {
						return false;
					}
					model->addVertex(vertex);
				}
				else if (std::string(modelNode->name()) == "union") {

				}
				else if (std::string(modelNode->name()) == "color") {

				}
			}
		}
		return true;
	}

	bool Tool::parseVertex(xml_Node * vertexNode, std::shared_ptr<Vector2> vertex) {
		std::string nameNode;
		for (xml_Node * vertNode = vertexNode->first_node(); vertNode; vertNode = vertNode->next_sibling()) {
			nameNode = vertNode->value();
			//if (vertNode->type() == node_element) {
				if (std::string(vertNode->name()) == "v") {
					size_t nComa = std::count(nameNode.begin(), nameNode.end(), ',');
					std::vector<float> values;
					for (size_t i = 0; i < nComa + 1; ++i) {
						size_t position;
						position = nameNode.find(',');
						values.push_back(std::stof(nameNode.substr(0, position)));
						nameNode.erase(0, position + 1);
					}
					vertex->set(values[0], values[1]);
					//vertex->x = values[0];
					//vertex->y = values[1];
					

				}

			//}
		}
		return true;
	}
}