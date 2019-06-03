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
		void fillVectors(int layer, int model, Vector2 vectors[]);

	public:
		inline int getLayersAmount() 
		{
			return (int)layers.size();
		}

		inline const std::string getLayerName(int layer)
		{
			return layers[layer]->getName();
		}

		inline const int getModelsInLayerAmount(int layer)
		{
			return layers[layer]->getModelsAmount();
		}

		inline const std::string getModelNameInLayer(int layer, int model)
		{
			return layers[layer]->getModelName(model);
		}

		inline const int getVectorsAmount(int layer, int model) 
		{
			return layers[layer]->getVectorsAmount(model);
		}

		


		
	};
}

#endif // !GONIC_HEADER
