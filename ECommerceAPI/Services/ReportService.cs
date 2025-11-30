using ECommerceAPI.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ECommerceAPI.Services;

internal sealed class ReportService(IProductService productService) : IReportService
{
    public void GenerateProductReport(Stream stream)
    {
        var skip = 0;
        var take = 50;

        List<Product> products = [];
        bool hasMore;

        do
        {
            var list = productService.GetProducts(skip, take).ToList();
            
            products.AddRange(list);
            
            hasMore = list.Count == take;
            skip += take;
        } while (hasMore);
        
        var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(columns =>
                        {
                            columns.Item()
                                .Text("Product Report")
                                .SemiBold()
                                .FontSize(36)
                                .FontColor(Colors.Blue.Medium);
                            
                            columns.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40);
                                    columns.RelativeColumn();
                                });
                                
                                table.Header(header =>
                                {
                                    header.Cell().BorderBottom(2).Padding(8).Text("Id");
                                    header.Cell().BorderBottom(2).Padding(8).Text("Product Name");
                                });
                                
                                foreach (var product in products)
                                {

                                    table.Cell().Padding(2).PaddingLeft(8).Text($"{product.ID}");
                                    table.Cell().Padding(2).PaddingLeft(8).Text(product.Name);
                                }
                            });
                        }); 

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                        });
                });
            });
        
        document.GeneratePdf(stream);
    }
}