/**
 * @file Vector2.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief A basic struct class for Vector2
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef VECTOR2_HEADER
#define VECTOR2_HEADER

namespace tool 
{

	/**
	 * @brief A basic struct class for Vector2
	 * 
	 */
	struct Vector2 
	{

	public:
		/**
		 * @brief X. Second value.
		 * 
		 */
		float x;

		/**
		 * @brief Y. Second value.
		 * 
		 */
		float y;

	public:
		/**
		 * @brief Construct a new Vector 2 object
		 * 
		 * @param x 
		 * @param y 
		 */
		Vector2(float x = 0.0f, float y = 0.0f);

		/**
		 * @brief Construct a new Vector 2 object from other
		 * 
		 * @param other 
		 */
		Vector2(const Vector2& other);

		/**
		 * @brief Se the X and Y values
		 * 
		 * @param _x 
		 * @param _y 
		 */
		void set(float _x, float _y);

		/**
		 * @brief Overload of operator = to equal one Vector to other
		 * 
		 * @return Vector2 
		 */
		Vector2 operator=(const Vector2 &);
	};
}

#endif // !VECTOR2_HEADER
