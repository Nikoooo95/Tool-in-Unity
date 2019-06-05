#ifndef UNION_HEADER
#define UNION_HEADER

namespace tool {
	class Union {
	private:
		int pointA;
		int pointB;
	public:
		Union(int pA, int pB) : pointA(pA), pointB(pB){}

		void set(int pA, int pB) 
		{
			pointA = pA;
			pointB = pB;
		}

		int getA() { return pointA; }
		int getB() { return pointB; }

	};
}

#endif // !UNION_HEADER
