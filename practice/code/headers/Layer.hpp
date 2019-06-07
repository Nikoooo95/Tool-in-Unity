/**
 * @file Layer.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Layer class. From it inherits the Layer 2D and Layer 3D-
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef LAYER_HEADER
#define LAYER_HEADER

#include <map>
#include <string>

#include "Model2D.hpp"
#include "Model3D.hpp"

namespace tool 
{
	/**
	 * @brief Layer class. From it inherits the Layer 2D and Layer 3D-
	 * 
	 */
	class Layer 
	{

	private:
		/**
		 * @brief Name of the class
		 * 
		 */
		std::string name;

	protected:
		/**
		 * @brief Construct a new Layer object
		 * 
		 * @param name_ 
		 */
		Layer(std::string name_) : name(name_){}

	public:
		/**
		 * @brief Get the Name of the layer
		 * 
		 * @return const std::string 
		 */
		inline const std::string getName()
		{
			return name;
		}

	public:
		/**
		 * @brief Get the Color of the layer
		 * 
		 * @param model 
		 * @param color 
		 */
		virtual void getColor(int model, Color* color) = 0;
		
	};
}

#endif // !LAYER_HEADER
