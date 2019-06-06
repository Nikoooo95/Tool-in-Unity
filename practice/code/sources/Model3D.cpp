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

	

}