#ifndef GONIC_HEADER
#define GONIC_HEADER

#include "Layer.hpp"

namespace tool {
	class Gonic {
	private:
		std::vector<std::shared_ptr<Layer>> layers;
	public:
		Gonic() {}

	public:
		void addLayer(std::string name, std::shared_ptr<Layer> layer);

		std::string getFirstLayerName() {
			//std::map<std::string, std::shared_ptr<Layer>>::iterator it = layers.begin();
			return layers.front()->getName();
			/*if (layers.empty()) {
				return "el mapa esta vacio";
			}
			return "esta ocupao";*/
		}

		int getLayersAmount() {
			return layers.size();
		}

		const std::string getLayerName(int pos) {
			return layers[pos]->getName();
		}

		const int getModelsInLayerAmount(int pos) {
			return layers[pos]->getModelsAmount();
		}

		const int getVectorsAmount(int layer, int model) {
			return layers[layer]->getVectorsAmount(model);
		}
		const std::string getModelNameInLayer(int layer, int model) {
			return layers[layer]->getModelName(model);
		}

		void fillVectors(int layer, int model, Vector2 vectors[]) {
			layers[layer]->fillVectors(model, vectors);
		}


		
	};
}

#endif // !GONIC_HEADER
