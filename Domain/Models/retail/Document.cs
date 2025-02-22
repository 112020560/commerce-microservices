using Domain.Models.crm;

namespace Domain.Models.retail;

public partial class Document
{
    public Guid Id { get; set; }

    public Guid? DocumentTypeId { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateTime? Date { get; set; }

    public Guid? PersonId { get; set; }

    public decimal Total { get; set; }

    public Guid? StatusId { get; set; }

    public virtual ICollection<DocumentDetail> DocumentDetails { get; set; } = new List<DocumentDetail>();

    public virtual DocumentType? DocumentType { get; set; }

    public virtual Person? Person { get; set; }

    public virtual DocumentStatus? Status { get; set; }
}
