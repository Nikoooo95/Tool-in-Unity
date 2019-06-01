#include "Gonic.hpp"
namespace tool {
	void Gonic::addLayer(std::string name, std::shared_ptr<Layer> layer) {
		layers[name] = layer;
	}
}