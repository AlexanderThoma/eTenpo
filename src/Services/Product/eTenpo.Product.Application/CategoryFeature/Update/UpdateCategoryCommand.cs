using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Update;

public record UpdateCategoryCommand : ICommand<UpdateCategoryCommandResponse>
{
    public UpdateCategoryCommand()
    {
        this.Id = Guid.Empty;
        this.Name = string.Empty;
        this.Description = string.Empty;
    }
    
    public UpdateCategoryCommand(Guid Id,
        string Name,
        string Description)
    {
        this.Name = Name;
        this.Description = Description;
        this.Id = Id;
    }

    // used for mapping the id in the controller
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }

    public void Deconstruct(out Guid Id, out string Name, out string Description)
    {
        Id = this.Id;
        Name = this.Name;
        Description = this.Description;
    }
}