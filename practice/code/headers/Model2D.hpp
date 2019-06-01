#ifndef MODEL_2D_HEADER
#define MODEL_2D_HEADER
#include "Model.hpp"
namespace tool {
	class Model2D {
	private:
		std::vector<std::shared_ptr<Vertex2D>> vertex;
		std::string name;
	public:
		Model2D(std::string _name) : name(_name){}
	public:
		void addVertex(std::shared_ptr<Vertex2D> vert){
			vertex.push_back(vert);
		}
	};
}

#endif // !MODEL_2D_HEADER
