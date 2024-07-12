namespace AgendaTenis.Jogadores.Core.Enums;

public enum CategoriaEnum
{
    Atp = 1,
    Avancado = 2,
    Intermediario = 3,
    Iniciante = 4
}

public class CategoriaEnumModel : BaseEnumModel<CategoriaEnum>
{
    public CategoriaEnumModel(CategoriaEnum id) : base(id)
    {
    }

    protected override string ObterDescricao()
    {
        return Id switch
        {
            CategoriaEnum.Atp => "Atp",
            CategoriaEnum.Avancado => "Avancado",
            CategoriaEnum.Intermediario => "Intermediario",
            CategoriaEnum.Iniciante => "Iniciante",
            _ => throw new ArgumentOutOfRangeException(nameof(Id), Id, null)
        };
    }
}