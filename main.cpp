#include <iostream>

unsigned int silnia(unsigned int & x)
{
	for (int i = x - 1; i > 0; i--)
	{
		x *= i;
	}
	return x;
}

int symbol_newtona(unsigned int N, unsigned int K)
{
	if (N == K) return 1;
	int iloczyn = 1;
	for (int i = N - K + 1; i <= N; i++)
	{
		iloczyn *= i;
	}

	double outcome = (float)iloczyn / (float)silnia(K);
	return outcome;
}

int main()
{
	int d;
	unsigned int N, K;
	std::cin >> d;
	for (int i = 0; i < d; i++)
	{
		std::cin >> N >> K;
		if (symbol_newtona(N, K) % 2 == 0) std::cout << "P";
		else std::cout << "N";
		std::cout << "\n";
	}
	return 0;
}
