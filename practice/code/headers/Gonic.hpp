/**
 * @file Gonic.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Gonic file. Contains all the 2D layers and 3D layers of a gonic file.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef GONIC_HEADER
#define GONIC_HEADER

#include "Layer2D.hpp"
#include "Layer3D.hpp"

namespace tool {
	/**
	 * @brief Gonic file. Contains all the 2D layers and 3D layers of a gonic file.
	 * 
	 */
	class Gonic {
	private:
		/**
		 * @brief Vector of the 2D layers
		 * 
		 */
		std::vector<std::shared_ptr<Layer2D>> layers2D;

		/**
		 * @brief Vector of the 3D layers
		 * 
		 */
		std::vector<std::shared_ptr<Layer3D>> layers3D;

	public:
		/**
		 * @brief Construct a new Gonic object
		 * 
		 */
		Gonic() {}

	public:
		/**
		 * @brief Adds a new 2d layer to the vector
		 * 
		 * @param name 
		 * @param layer 
		 */
		void addLayer2d(std::string name, std::shared_ptr<Layer2D> layer);

		/**
		 * @brief Adds a new 3d layer to the vector
		 * 
		 * @param name 
		 * @param layer 
		 */
		void addLayer3d(std::string name, std::shared_ptr<Layer3D> layer);

		/**
		 * @brief Fills a Vector2 with the vertex of a 2D Model
		 * 
		 * @param layer 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors2d(int layer, int model, Vector2 vectors[]);

		/**
		 * @brief Fills a Vector3 with the vertex of a 3D model
		 * 
		 * @param layer 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors3d(int layer, int model, Vector3 vectors[]);

		/**
		 * @brief Get the Color from a Model
		 * 
		 * @param layer 
		 * @param model 
		 * @param color 
		 */
		void getColor(int layer, int model, Color* color);

	public:
		/**
		 * @brief Get the Layers2d Amount
		 * 
		 * @return int 
		 */
		inline int getLayers2dAmount() 
		{
			return (int)layers2D.size();
		}

		/**
		 * @brief Get the Layer2d Name
		 * 
		 * @param layer 
		 * @return const std::string 
		 */
		inline const std::string getLayer2dName(int layer)
		{
			return layers2D[layer]->getName();
		}

		/**
		 * @brief Get the Models Amount In Layer2d
		 * 
		 * @param layer 
		 * @return const int 
		 */
		inline const int getModelsInLayer2dAmount(int layer)
		{
			return layers2D[layer]->getModels2dAmount();
		}

		/**
		 * @brief Get the Model Name In Layer2d 
		 * 
		 * @param layer 
		 * @param model 
		 * @return const std::string 
		 */
		inline const std::string getModelNameInLayer2d(int layer, int model)
		{
			return layers2D[layer]->getModel2dName(model);
		}

		/**
		 * @brief Get the Vectors Amount of a Model
		 * 
		 * @param layer 
		 * @param model 
		 * @return const int 
		 */
		inline const int getVectorsAmount(int layer, int model) 
		{
			return layers2D[layer]->getVectorsAmount(model);
		}

		/**
		 * @brief Get the Layer2D object from a Layer
		 * 
		 * @param layer 
		 * @return const std::shared_ptr<Layer2D> 
		 */
		inline const std::shared_ptr<Layer2D> getLayer2D(int layer) {
			return layers2D[layer];
		}

		/**
		 * @brief Get the Layer3D object from a Layer
		 * 
		 * @param layer 
		 * @return const std::shared_ptr<Layer3D> 
		 */
		inline const std::shared_ptr<Layer3D> getLayer3D(int layer) {
			return layers3D[layer];
		}

		/**
		 * @brief Get the Model 2D In Layer
		 * 
		 * @param layer 
		 * @param model 
		 * @return const std::shared_ptr<Model2D> 
		 */
		inline const std::shared_ptr<Model2D> getModelInLayer2D(int layer, int model) 
		{
			return layers2D[layer]->getModel2d(model);
		}
		
	};
}

#endif // !GONIC_HEADER
