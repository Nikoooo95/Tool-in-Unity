#ifndef TOOL_HEADER
#define TOOL_HEADER

#include <string>
#include <rapidxml.hpp>
#include <fstream>
#include <vector>

#include "Model.hpp"
#include "Layer.hpp"
#include "Gonic.hpp"
#include "Model2D.hpp"
#include "Vector2.hpp"

using namespace rapidxml;

namespace tool 
{

	class Tool 
	{

	private:
		typedef rapidxml::xml_node<> xml_Node;
		typedef rapidxml::xml_attribute<> xml_Attribute;

	private:
		std::shared_ptr<Gonic> gonicFile;

	public:
		Tool() {}
		~Tool();

	public:
		bool parse(const std::string & path);
		bool parseFile(xml_Node * fileNode);
		bool parseLayer(xml_Node * layerNode, std::shared_ptr<Layer> layer);
		bool parseModel(xml_Node * modelNode, std::shared_ptr<Model2D> model);
		bool parseVertex(xml_Node * vertexNode, std::shared_ptr<Model2D> model);

	public:
		inline const std::string getLayerName(int layer) 
		{		
			return gonicFile->getLayerName(layer); 
		}

		inline const int getLayersAmount() 
		{				
			return gonicFile->getLayersAmount(); 
		}

		inline const int getModelsInLayerAmount(int layer) 
		{ 
			return gonicFile->getModelsInLayerAmount(layer); 
		}

		inline const int getVectorsAmount(int layer, int model) 
		{ 
			return gonicFile->getVectorsAmount(layer, model); 
		}

		inline const std::string getModelNameInLayer(int layer, int model) { 
			return gonicFile->getModelNameInLayer(layer, model); 
		}

		void fillVectors(int layer, int model, Vector2 vectors[]) { 
			gonicFile->fillVectors(layer, model, vectors); 
		}

		const char* charToString(std::string dataValue) {
			int lenStr = dataValue.length() + 1;
			char* answer = new char[lenStr];
			const char * constAnswer = new char[lenStr];
			strcpy_s(answer, lenStr, dataValue.c_str());
			constAnswer = answer;
			return constAnswer;
		}
	};
}

#endif // !TOOL_HEADER
