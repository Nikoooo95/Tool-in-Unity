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
#include "Color.hpp"
#include "Union.hpp"

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
		~Tool() {}

	public:
		bool parse(const std::string & path);
		bool parseFile(xml_Node * fileNode);
		bool parseLayer(xml_Node * layerNode, std::shared_ptr<Layer2D> layer);
		bool parseModel(xml_Node * modelNode, std::shared_ptr<Model2D> model);
		bool parseVertex(xml_Node * vertexNode, std::shared_ptr<Model2D> model);
		bool parseColor(xml_Node * colorNode, std::shared_ptr<Model2D> model);

	public:
		inline const std::string getLayer2dName(int layer) 
		{		
			return gonicFile->getLayer2dName(layer); 
		}

		inline const int getLayers2dAmount() 
		{				
			return gonicFile->getLayers2dAmount(); 
		}

		inline const int getModelsInLayer2dAmount(int layer) 
		{ 
			return gonicFile->getModelsInLayer2dAmount(layer); 
		}

		inline const int getVectorsAmount(int layer, int model) 
		{ 
			return gonicFile->getVectorsAmount(layer, model); 
		}

		inline const std::string getModelNameInLayer2d(int layer, int model) { 
			return gonicFile->getModelNameInLayer2d(layer, model); 
		}

		void fillVectors2d(int layer, int model, Vector2 vectors[]) { 
			gonicFile->fillVectors2d(layer, model, vectors); 
		}

		void fillVectors3d(int layer, int model, Vector3 vectors[]) {
			gonicFile->fillVectors3d(layer, model, vectors);
		}

		void getColor(int layer, int model, Color* color) {
			gonicFile->getColor(layer, model, color);
		}

		const char* charToString(std::string dataValue);

		void generateLayer3d(int layer);

		void transform2dTo3d(int layer, int model);
		
	};
}

#endif // !TOOL_HEADER
