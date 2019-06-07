/**
 * @file Color.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief A basic struct class for Color
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef COLOR_HEADER
#define COLOR_HEADER

namespace tool 
{
	/**
	 * @brief Color structs. It is a RGB color in format 0 to 1.
	 * 
	 */
	struct Color 
	{
	public:
		/**
		 * @brief Red value
		 * 
		 */
		float r;

		/**
		 * @brief Green value
		 * 
		 */
		float g;

		/**
		 * @brief Blue value
		 * 
		 */
		float b;

	public:
		
		/**
		 * @brief Construct a new Color object
		 * 
		 * @param r -> Red
		 * @param g -> Green
		 * @param b -> Blue
		 */
		Color(float r = 0, float g = 0, float b = 0);

		/**
		 * @brief Construct a new Color object from other color
		 * 
		 * @param other 
		 */
		Color(const Color& other);

		/**
		 * @brief Sets the color values
		 * 
		 * @param r -> Red
		 * @param g -> Green
		 * @param b -> Blue
		 */
		void set(float r, float g, float b);

		/**
		 * @brief Overload = to makes a color equal to other
		 * 
		 * @return Color 
		 */
		Color operator=(const Color &);

	
	};
}

#endif // !COLOR_HEADER
