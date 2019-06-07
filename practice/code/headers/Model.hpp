/**
 * @file Model.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Model class. From it inherits the Model2D and Model3D
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef MODEL_HEADER
#define MODEL_HEADER

#include <string>
#include <vector>

#include "Color.hpp"
#include "Vector2.hpp"
#include "Vector3.hpp"

namespace tool 
{
	/**
	 * @brief Model class. From it inherits the Model2D and Model3D
	 * 
	 */
	class Model 
	{
	private:
		/**
		 * @brief Shared Pointer from Color of the model
		 * 
		 */
		std::shared_ptr<Color> color;

		/**
		 * @brief Name of the model
		 * 
		 */
		std::string name;

		/**
		 * @brief Height of the model
		 * 
		 */
		float height;
		
	protected:
		/**
		 * @brief Construct a new Model object
		 * 
		 * @param name_ 
		 * @param height_ 
		 */
		Model(std::string name_, float height_): name(name_), height(height_) {}

	public:
		/**
		 * @brief Get the Name of the model
		 * 
		 * @return const std::string 
		 */
		inline const std::string getName()
		{
			return name;
		}

		/**
		 * @brief Set the Color of the model
		 * 
		 * @param newColor 
		 */
		void setColor(std::shared_ptr<Color> newColor)
		{
			color = newColor;
		}

		/**
		 * @brief Modifies the color of the model
		 * 
		 * @param color_ 
		 */
		void modifyColor(Color* color_)
		{
			color_->set(color->r, color->g, color->b);
		}

		/**
		 * @brief Get the Color object. Shared pointer
		 * 
		 * @return std::shared_ptr<Color> 
		 */
		std::shared_ptr<Color> getColor() 
		{
			return color;
		}

		/**
		 * @brief Set the Height of the model
		 * 
		 * @param height_ 
		 */
		void setHeight(float height_) {
			height = height_;
		}

		/**
		 * @brief Get the Height of the model
		 * 
		 * @return float 
		 */
		inline float getHeight() {
			return height;
		}

		/**
		 * @brief Get the Vectors Amount of the model
		 * 
		 * @return int 
		 */
		virtual int getVectorsAmount() = 0;

	};
}
	


#endif // !MODEL_HEADER
