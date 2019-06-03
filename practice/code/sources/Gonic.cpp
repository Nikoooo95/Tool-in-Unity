#include "Gonic.hpp"
namespace tool 
{

	void Gonic::addLayer(std::string name, std::shared_ptr<Layer> layer) 
	{
		layers.push_back(layer);
	}

	void Gonic::fillVectors(int layer, int model, Vector2 vectors[])
	{
		layers[layer]->fillVectors(model, vectors);
	}

}