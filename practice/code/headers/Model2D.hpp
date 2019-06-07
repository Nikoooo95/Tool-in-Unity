/**
 * @file Model2D.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Model 2D. Contains all the info a 2D model from gonic file.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef MODEL_2D_HEADER
#define MODEL_2D_HEADER

#include "Model.hpp"

namespace tool 
{
	/**
	 * @brief Model 2D. Contains all the info a 2D model from gonic file.
	 * 
	 */
	class Model2D : public Model
	{
	private:
		/**
		 * @brief Vector of shared pointer of Vector2
		 * 
		 */
		std::vector<std::shared_ptr<Vector2>> vertex;

	public:
		/**
		 * @brief Construct a new Model 2D object
		 * 
		 * @param name_ 
		 * @param height_ 
		 */
		Model2D(std::string name_, float height_) : Model(name_, height_){}

	public:
		/**
		 * @brief Add a vertex to the vector
		 * 
		 * @param vert 
		 */
		void addVertex(std::shared_ptr<Vector2> vert);

		/**
		 * @brief Fill a Vector2 array with the vertex of the model
		 * 
		 * @param _vectors 
		 */
		void fillVectors(Vector2 _vectors[]);


	public:
		/**
		 * @brief Get the Vectors Amount of the model
		 * 
		 * @return int 
		 */
		inline int getVectorsAmount() override
		{
			return (int)vertex.size();
		}

		/**
		 * @brief Get the Vector object of a concrete position
		 * 
		 * @param pos 
		 * @return std::shared_ptr<Vector2> 
		 */
		std::shared_ptr<Vector2> getVector(int pos)
		{
			return vertex[pos];
		}


	};
}

#endif // !MODEL_2D_HEADER
