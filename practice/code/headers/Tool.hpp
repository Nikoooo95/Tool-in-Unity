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
#include "Vertex2D.hpp"

using namespace rapidxml;

namespace tool {
	class Tool {
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
		bool parseVertex(xml_Node * vertexNode, std::shared_ptr<Vertex2D> vertex);

	public:
		const std::string getLayerName() {
			return gonicFile->getFirstLayerName();

		}
	};
}

#endif // !TOOL_HEADER
