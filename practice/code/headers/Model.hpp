#ifndef MODEL_HEADER
#define MODEL_HEADER
#include <string>
#include <vector>
#include "Vertex2D.hpp"
namespace tool {
	class Model {
	private:
		std::string name;
		
	public:
		Model(std::string _name): name(_name) {}

	protected:
		//virtual void addVertex();

	};
}
	


#endif // !MODEL_HEADER
