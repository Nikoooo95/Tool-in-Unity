/**
 * @file Vector3.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief A basic struct class for Vector3
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef VECTOR3_HEADER
#define VECTOR3_HEADER

namespace tool
{

	/**
	 * @brief A basic struct class for Vector3
	 * 
	 */
	struct Vector3
	{

	public:
		/**
		 * @brief X. First value.
		 * 
		 */
		float x;

		/**
		 * @brief Y. Second value.
		 * 
		 */
		float y;

		/**
		 * @brief Z. Third value.
		 * 
		 */
		float z;

	public:
		/**
		 * @brief Construct a new Vector 3 object
		 * 
		 * @param x 
		 * @param y 
		 * @param z 
		 */
		Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f);

		/**
		 * @brief Construct a new Vector 3 object from other
		 * 
		 * @param other 
		 */
		Vector3(const Vector3& other);

		/**
		 * @brief Sets the values for the Vector
		 * 
		 * @param x_ 
		 * @param y_ 
		 * @param z_ 
		 */
		void set(float x_, float y_, float z_);

		/**
		 * @brief Overload of operator = to equal one Vector to other
		 * 
		 * @return Vector3 
		 */
		Vector3 operator=(const Vector3 &);
	};
}

#endif // !VECTOR3_HEADER
