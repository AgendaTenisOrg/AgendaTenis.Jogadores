using AgendaTenis.Jogadores.Core.Enums;

namespace AgendaTenis.Jogadores.Core.Regras;

public static class CategoriaRegras
{
    public static double ObterPontuacaoMinima(this CategoriaEnum categoria)
    {
        switch (categoria)
        {
            case CategoriaEnum.Atp:
                {
                    return 1000;
                }
            case CategoriaEnum.Avancado:
                {
                    return 750;
                }
            case CategoriaEnum.Intermediario:
                {
                    return 500;
                }
            case CategoriaEnum.Iniciante:
                {
                    return 0;
                }
            default:
                {
                    return 0;
                };
        }
    }

    public static double ObterPontuacaoMaxima(this CategoriaEnum categoria)
    {
        switch (categoria)
        {
            case CategoriaEnum.Atp:
                {
                    return double.MaxValue;
                }
            case CategoriaEnum.Avancado:
                {
                    return 1000;
                }
            case CategoriaEnum.Intermediario:
                {
                    return 750;
                }
            case CategoriaEnum.Iniciante:
                {
                    return 500;
                }
            default:
                {
                    return double.MaxValue;
                };
        }
    }

    public static CategoriaEnum ObterCategoria(this double pontuacao)
    {
        if (pontuacao >= 1000)
        {
            return CategoriaEnum.Atp;
        }
        else if (pontuacao >= 750)
        {
            return CategoriaEnum.Avancado;
        }
        else if (pontuacao >= 500)
        {
            return CategoriaEnum.Intermediario;
        }
        else
        {
            return CategoriaEnum.Iniciante;
        }
    }
}
