#include "Layer2D.hpp"

namespace tool
{
	void Layer2D::addModel(std::string name, std::shared_ptr<Model2D> model)
	{
		models.push_back(model);
	}

	void Layer2D::fillVectors(int model, Vector2 vectors[])
	{
		models[model]->fillVectors(vectors);
	}
}