#ifndef GONIC_HEADER
#define GONIC_HEADER

#include "Layer2D.hpp"
#include "Layer3D.hpp"

namespace tool {
	class Gonic {
	private:
		std::vector<std::shared_ptr<Layer2D>> layers2D;
		std::vector<std::shared_ptr<Layer3D>> layers3D;

	public:
		Gonic() {}

	public:
		void addLayer2d(std::string name, std::shared_ptr<Layer2D> layer);
		void addLayer3d(std::string name, std::shared_ptr<Layer3D> layer);
		void fillVectors2d(int layer, int model, Vector2 vectors[]);
		void fillVectors3d(int layer, int model, Vector3 vectors[]);
		void getColor(int layer, int model, Color* color);

	public:
		inline int getLayers2dAmount() 
		{
			return (int)layers2D.size();
		}

		inline const std::string getLayer2dName(int layer)
		{
			return layers2D[layer]->getName();
		}

		inline const int getModelsInLayer2dAmount(int layer)
		{
			return layers2D[layer]->getModels2dAmount();
		}

		inline const std::string getModelNameInLayer2d(int layer, int model)
		{
			return layers2D[layer]->getModel2dName(model);
		}

		inline const int getVectorsAmount(int layer, int model) 
		{
			return layers2D[layer]->getVectorsAmount(model);
		}

		inline const std::shared_ptr<Layer2D> getLayer2D(int layer) {
			return layers2D[layer];
		}

		inline const std::shared_ptr<Layer3D> getLayer3D(int layer) {
			return layers3D[layer];
		}

		inline const std::shared_ptr<Model2D> getModelInLayer2D(int layer, int model) 
		{
			return layers2D[layer]->getModel2d(model);
		}

		


		
	};
}

#endif // !GONIC_HEADER
