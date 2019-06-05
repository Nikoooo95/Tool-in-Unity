#include "Layer3D.hpp"

namespace tool
{
	void Layer3D::addModel(std::string name, std::shared_ptr<Model3D> model)
	{
		models.push_back(model);
	}

	void Layer3D::fillVectors(int model, Vector3 vectors[])
	{
		models[model]->fillVectors(vectors);
	}
}