#include "Layer.hpp"

namespace tool {
	void Layer::addModel(std::string name, std::shared_ptr<Model2D> model) {
		models[name] = model;
	}
}