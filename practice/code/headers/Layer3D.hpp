/**
 * @file Layer3D.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Layer 3D class. Contains all the stuff in 3D which comes from a Layer2D.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef LAYER_3D_HEADER
#define LAYER_3D_HEADER

#include "Layer.hpp"

namespace tool
{
	/**
	 * @brief Layer 3D class. Contains all the stuff in 3D which comes from a Layer2D.
	 * 
	 */
	class Layer3D : public Layer
	{
	private:
		/**
		 * @brief Vector of shared pointer to 3D models
		 * 
		 */
		std::vector<std::shared_ptr<Model3D>> models;

	public:
		/**
		 * @brief Construct a new Layer 3D object
		 * 
		 * @param name_ 
		 */
		Layer3D(std::string name_) : Layer(name_){}

	public:
		/**
		 * @brief Get the Models3d Amount of the layer
		 * 
		 * @return const int 
		 */
		inline const int getModels3dAmount()
		{
			return (int)models.size();
		}

		/**
		 * @brief Get the Vectors3d Amount from a 3d Model
		 * 
		 * @param model 
		 * @return const int 
		 */
		inline const int getVectors3dAmount(int model)
		{
			return models[model]->getVectorsAmount();
		}

		/**
		 * @brief Get the Model3d Name
		 * 
		 * @param model 
		 * @return const std::string 
		 */
		inline const std::string getModel3dName(int model)
		{
			return models[model]->getName();
		}

		/**
		 * @brief Get the color of a model in the layer
		 * 
		 * @param model 
		 * @param color 
		 */
		void Layer::getColor(int model, Color* color)
		{
			models[model]->modifyColor(color);
		}

		/**
		 * @brief Add a model to the vector
		 * 
		 * @param name 
		 * @param model 
		 */
		void addModel(std::string name, std::shared_ptr<Model3D> model);

		/**
		 * @brief Fill a Vector3 array with the vertex of a 3D model
		 * 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors(int model, Vector3 vectors[]);
	};
}

#endif // !LAYER_3D_HEADER
