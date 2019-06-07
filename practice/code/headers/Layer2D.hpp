/**
 * @file Layer2D.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Layer class. Contains all the 2D models from a Gonic file.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef LAYER_2D_HEADER
#define LAYER_2D_HEADER

#include "Layer.hpp"

namespace tool
{
	/**
	 * @brief Layer class. Contains all the 2D models from a Gonic file.
	 * 
	 */
	class Layer2D : public Layer
	{
	private:
		/**
		 * @brief A vector of Shared Pointers to 2D Models
		 * 
		 */
		std::vector<std::shared_ptr<Model2D>> models;

	public:
		/**
		 * @brief Construct a new Layer 2D object
		 * 
		 * @param name_ 
		 */
		Layer2D(std::string name_) : Layer(name_){}

	public:
		/**
		 * @brief Get the Models2d Amount of the Layer
		 * 
		 * @return const int 
		 */
		inline const int getModels2dAmount()
		{
			return (int)models.size();
		}

		/**
		 * @brief Get the Vectors Amount from a Model
		 * 
		 * @param model 
		 * @return const int 
		 */
		inline const int getVectorsAmount(int model)
		{
			return models[model]->getVectorsAmount();
		}

		/**
		 * @brief Get the Model2d Name
		 * 
		 * @param model 
		 * @return const std::string 
		 */
		inline const std::string getModel2dName(int model)
		{
			return models[model]->getName();
		}

		/**
		 * @brief Get the colo of a model
		 * 
		 * @param model 
		 * @param color 
		 */
		void Layer::getColor(int model, Color* color)
		{
			models[model]->modifyColor(color);
		}

		/**
		 * @brief Add a model 2d to the vector
		 * 
		 * @param name 
		 * @param model 
		 */
		void addModel(std::string name, std::shared_ptr<Model2D> model);

		/**
		 * @brief Fills a Vector2 with the vertex of a 2D model
		 * 
		 * @param model 
		 * @param vectors 
		 */
		void fillVectors(int model, Vector2 vectors[]);

		/**
		 * @brief Get the Model2d object from the Layer
		 * 
		 * @param model 
		 * @return const std::shared_ptr<Model2D> 
		 */
		inline const std::shared_ptr<Model2D> getModel2d(int model) 
		{
			return models[model];
		}

	};
}

#endif // !LAYER_2D_HEADER
