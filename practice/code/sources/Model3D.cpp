#include "Model3D.hpp"

namespace tool
{

	void Model3D::addVertex(std::shared_ptr<Vector3> vert)
	{
		vertex.push_back(vert);
	}

	void Model3D::fillVectors(Vector3 vectors[])
	{
		for (int i = 0; i < getVectorsAmount(); ++i) 
		{
			vectors[i].set(vertex[i]->x, vertex[i]->y, vertex[i]->z);
		}
	}

	void Model3D::generateTriangles(int amount)
	{
		int trianglesAmount = amount * 3;
		for (int i = 0; i < trianglesAmount; i += 2)
		{
			triangles.push_back(i);
			triangles.push_back(i + 2);
			triangles.push_back(i + 1);

			triangles.push_back(i + 1);
			triangles.push_back(i + 2);
			triangles.push_back(i + 3);
		}
	}

}