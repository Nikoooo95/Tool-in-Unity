#ifndef GONIC_HEADER
#define GONIC_HEADER

#include "Layer.hpp"

namespace tool {
	class Gonic {
	private:
		std::map<std::string, std::shared_ptr<Layer>> layers;
	public:
		Gonic() {}

	public:
		void addLayer(std::string name, std::shared_ptr<Layer> layer);

		std::string getFirstLayerName() {
			//std::map<std::string, std::shared_ptr<Layer>>::iterator it = layers.begin();
			return layers.begin()->first;
			/*if (layers.empty()) {
				return "el mapa esta vacio";
			}
			return "esta ocupao";*/
		}
	};
}

#endif // !GONIC_HEADER
