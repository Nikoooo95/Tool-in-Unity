#include "Layer.hpp"

namespace tool 
{

	void Layer::addModel(std::string name, std::shared_ptr<Model2D> model)
	{
		models.push_back(model);
	}

	void Layer::fillVectors(int model, Vector2 vectors[])
	{
		models[model]->fillVectors(vectors);
	}
}