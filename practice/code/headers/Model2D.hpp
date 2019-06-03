#ifndef MODEL_2D_HEADER
#define MODEL_2D_HEADER
#include "Model.hpp"
namespace tool {
	class Model2D {
	private:
		std::vector<std::shared_ptr<Vector2>> vertex;
		std::string name;
	public:
		Model2D(std::string _name) : name(_name){}
	public:
		void addVertex(std::shared_ptr<Vector2> vert){
			vertex.push_back(vert);
		}

		const std::string getName() {
			return name;
		}

		int getVectorsAmount() {
			return vertex.size();
		}

		void fillVectors(Vector2 _vectors[]) {
			
			for (int i = 0; i < sizeof(_vectors); ++i) {
				_vectors[i].set(vertex[i]->x, vertex[i]->y);
			}
			
			//models[model]->fillVectors(vectors);
		}
	};
}

#endif // !MODEL_2D_HEADER
