using HKSupply.Models.Supply;
using System.Collections.Generic;

namespace HKSupply.Services.Interfaces
{
    public interface IDocHeadAttachFile
    {
        List<DocHeadAttachFile> GetDocHeadAttachFile(string idDoc);
        DocHeadAttachFile AddDocHeadAttachFile(DocHeadAttachFile attachFile);
    }
}
