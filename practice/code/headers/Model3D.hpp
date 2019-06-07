/**
 * @file Model3D.hpp
 * @author Nicolás Tapia Sanz (nic.tap95@gmail.com)
 * @brief Model 3D. A transformation of the Model 2D.
 * @version 1.0
 * @date 06/06/2019
 * 
 * @copyright Copyright (c) 2019
 * 
 */
#ifndef MODEL_3D_HEADER
#define MODEL_3D_HEADER

#include "Model.hpp"


namespace tool
{
	/**
	 * @brief Model 3D. A transformation of the Model 2D.
	 * 
	 */
	class Model3D : public Model
	{
	private:
		/**
		 * @brief A vector of shared pointer to Vector3. Contains the vertex of the model.
		 * 
		 */
		std::vector<std::shared_ptr<Vector3>> vertex;
		/**
		 * @brief Vector of int. Contains the order of the vertex.
		 * 
		 */
		std::vector<int> triangles;

	public:
		/**
		 * @brief Construct a new Model 3D object
		 * 
		 * @param name_ 
		 * @param height_ 
		 */
		Model3D(std::string name_, float height_) : Model(name_, height_){}

	public:
		/**
		 * @brief Add a vertex to the vector
		 * 
		 * @param vert 
		 */
		void addVertex(std::shared_ptr<Vector3> vert);

		/**
		 * @brief Fill an array of Vector3 with the vertex of the model
		 * 
		 * @param vectors 
		 */
		void fillVectors(Vector3 vectors[]);

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

	};

}


#endif // !MODEL_3D_HEADER
