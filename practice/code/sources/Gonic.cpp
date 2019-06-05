#include "Gonic.hpp"
namespace tool 
{

	void Gonic::addLayer2d(std::string name, std::shared_ptr<Layer2D> layer) 
	{
		layers2D.push_back(layer);
	}

	void Gonic::addLayer3d(std::string name, std::shared_ptr<Layer3D> layer)
	{
		layers3D.push_back(layer);
	}

	void Gonic::fillVectors2d(int layer, int model, Vector2 vectors[])
	{
		layers2D[layer]->fillVectors(model, vectors);
	}

	void Gonic::fillVectors3d(int layer, int model, Vector3 vectors[])
	{
		layers3D[layer]->fillVectors(model, vectors);
	}

	void Gonic::getColor(int layer, int model, Color* color) 
	{
		layers2D[layer]->getColor(model, color);
	}

}