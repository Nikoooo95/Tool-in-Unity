/**
 * @file Tool.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Tool class. It is the main class and the most important. Contains all the use of the Tool and the DLL.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
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


using namespace rapidxml;

namespace tool 
{
	/**
	 * @brief Tool class. It is the main class and the most important. Contains all the use of the Tool and the DLL.
	 * 
	 */
	class Tool 
	{

	private:
		typedef rapidxml::xml_node<> xml_Node;
		typedef rapidxml::xml_attribute<> xml_Attribute;

	private:
		/**
		 * @brief Shared pointer to the Gonic File
		 * 
		 */
		std::shared_ptr<Gonic> gonicFile;

	public:
		/**
		 * @brief Construct a new Tool object
		 * 
		 */
		Tool() {}

		/**
		 * @brief Destroy the Tool object
		 * 
		 */
		~Tool() {}

	public:
		/**
		 * @brief Finds the gonic file and starts to parse it
		 * 
		 * @param path 
		 * @return true 
		 * @return false 
		 */
		bool parse(const std::string & path);

		/**
		 * @brief Starts to parse the gonic file inside of it.
		 * 
		 * @param fileNode 
		 * @return true 
		 * @return false 
		 */
		bool parseFile(xml_Node * fileNode);

		/**
		 * @brief Parses a layer of the gonic file
		 * 
		 * @param layerNode 
		 * @param layer 
		 * @return true 
		 * @return false 
		 */
		bool parseLayer(xml_Node * layerNode, std::shared_ptr<Layer2D> layer);

		/**
		 * @brief Parses a model of the gonic file
		 * 
		 * @param modelNode 
		 * @param model 
		 * @return true 
		 * @return false 
		 */
		bool parseModel(xml_Node * modelNode, std::shared_ptr<Model2D> model);

		/**
		 * @brief Parses the vertex of a model of the Gonic file
		 * 
		 * @param vertexNode 
		 * @param model 
		 * @return true 
		 * @return false 
		 */
		bool parseVertex(xml_Node * vertexNode, std::shared_ptr<Model2D> model);

		/**
		 * @brief Parses the color of a model of the Gonic file.
		 * 
		 * @param colorNode 
		 * @param model 
		 * @return true 
		 * @return false 
		 */
		bool parseColor(xml_Node * colorNode, std::shared_ptr<Model2D> model);

	public:
		/**
		 * @brief Get the Layer2d Name of a concrete layer
		 * 
		 * @param layer 
		 * @return const std::string 
		 */
		inline const std::string getLayer2dName(int layer) 
		{		
			return gonicFile->getLayer2dName(layer); 
		}

		/**
		 * @brief Get the Layers2d Amount of the gonic file
		 * 
		 * @return const int 
		 */
		inline const int getLayers2dAmount() 
		{				
			return gonicFile->getLayers2dAmount(); 
		}
		
		/**
		 * @brief Get the Models In Layer2d Amount from the gonic file
		 * 
		 * @param layer 
		 * @return const int 
		 */
		inline const int getModelsInLayer2dAmount(int layer) 
		{ 
			return gonicFile->getModelsInLayer2dAmount(layer); 
		}

		/**
		 * @brief Get the Vectors Amount object of a concrete model from a concrete layer of the gonic file
		 * 
		 * @param layer 
		 * @param model 
		 * @return const int 
		 */
		inline const int getVectorsAmount(int layer, int model) 
		{ 
			return gonicFile->getVectorsAmount(layer, model); 
		}

		/**
		 * @brief Get the Model Name In a Layer2d object from the gonic file
		 * 
		 * @param layer 
		 * @param model 
		 * @return const std::string 
		 */
		inline const std::string getModelNameInLayer2d(int layer, int model) { 
			return gonicFile->getModelNameInLayer2d(layer, model); 
		}

		/**
		 * @brief Fills an array of Vector2 with the vertex of a concrete 2d layer
		 * 
		 * @param layer 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors2d(int layer, int model, Vector2 vectors[]) { 
			gonicFile->fillVectors2d(layer, model, vectors); 
		}

		/**
		 * @brief Fills an array of Vector3 with the vertex of a concrete 3D layer
		 * 
		 * @param layer 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors3d(int layer, int model, Vector3 vectors[]) {
			gonicFile->fillVectors3d(layer, model, vectors);
		}

		/**
		 * @brief Get the Color object from a concrete model from a concrete layer
		 * 
		 * @param layer 
		 * @param model 
		 * @param color 
		 */
		void getColor(int layer, int model, Color* color) {
			gonicFile->getColor(layer, model, color);
		}

		/**
		 * @brief Convertes a char chain to a string
		 * 
		 * @param dataValue 
		 * @return const char* 
		 */
		const char* charToString(std::string dataValue);

		/**
		 * @brief Generates a 3D layer from a 2D layer
		 * 
		 * @param layer 
		 */
		void generateLayer3d(int layer);

		/**
		 * @brief Transform a 2D model into a 3D model
		 * 
		 * @param layer 
		 * @param model 
		 */
		void transform2dTo3d(int layer, int model);

		/**
		 * @brief Generates the triangles for a model
		 * 
		 * @param triangles 
		 * @param amount 
		 * @param backFaces 
		 * @param looped 
		 */
		void generateTriangles(int triangles[], int amount, bool backFaces, bool looped);
	};
}

#endif // !TOOL_HEADER
