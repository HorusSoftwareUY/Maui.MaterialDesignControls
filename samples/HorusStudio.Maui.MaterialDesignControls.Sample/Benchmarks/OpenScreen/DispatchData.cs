namespace HorusStudio.Maui.MaterialDesignControls.Sample.Benchmarks.OpenScreen;

/// <summary>
/// Data behind the "Northbeam Dispatch" home screen — a fictional US freight
/// carrier, invented so the benchmark opens something that looks like a real
/// client app instead of a control gallery.
///
/// Built once and shared: the benchmark times opening the screen, not
/// generating its data. Deterministic, so every run and both runtimes bind
/// exactly the same rows.
/// </summary>
public sealed class DispatchData
{
    public static DispatchData Shared { get; } = new();

    public IReadOnlyList<ShipmentRow> Shipments { get; }

    public IReadOnlyList<string> EquipmentTypes { get; } =
        new[] { "Dry van 53'", "Reefer 53'", "Flatbed 48'", "Step deck", "Box truck 26'" };

    public IReadOnlyList<string> ServiceLevels { get; } =
        new[] { "Standard (3-5 days)", "Expedited (1-2 days)", "Team expedited", "White glove" };

    private DispatchData()
    {
        string[] shippers =
        {
            "Lakeshore Appliance", "Buckeye Organics", "Summit Outdoor Co.", "Harbor Point Medical",
            "Redline Auto Parts", "Prairie Mills", "Copperleaf Home", "Bluegrass Beverages",
            "Granite State Tools", "Sunbelt Solar", "Keystone Paper", "Riverbend Pharma",
            "Ozark Furniture", "Tidewater Seafood", "Pinecrest Electronics", "Mesa Building Supply",
            "Northfield Dairy", "Ironwood Steel", "Cascade Coffee", "Liberty Textiles",
        };

        string[] lanes =
        {
            "Columbus, OH → Atlanta, GA", "Dayton, OH → Nashville, TN", "Chicago, IL → Dallas, TX",
            "Indianapolis, IN → Charlotte, NC", "Detroit, MI → Memphis, TN", "Pittsburgh, PA → Newark, NJ",
            "Cincinnati, OH → Jacksonville, FL", "Louisville, KY → Houston, TX", "Cleveland, OH → Boston, MA",
            "St. Louis, MO → Denver, CO",
        };

        (string Text, Color Color)[] statuses =
        {
            ("In transit", Color.FromArgb("#5B8CFF")),
            ("Delivered", Color.FromArgb("#2DD4BF")),
            ("At pickup", Color.FromArgb("#FFB547")),
            ("Delayed", Color.FromArgb("#F2545B")),
            ("Out for delivery", Color.FromArgb("#A78BFA")),
        };

        Color[] avatarColors =
        {
            Color.FromArgb("#3B4F7A"), Color.FromArgb("#5A3E7A"), Color.FromArgb("#2E6A6A"),
            Color.FromArgb("#7A5A2E"), Color.FromArgb("#7A2E45"), Color.FromArgb("#2E5A7A"),
        };

        var rows = new List<ShipmentRow>(shippers.Length);
        for (var i = 0; i < shippers.Length; i++)
        {
            // Mild skew toward "In transit", like a real board mid-afternoon.
            var status = statuses[(i * 7 + i / 3) % statuses.Length];
            var name = shippers[i];
            var words = name.Split(' ');
            var initials = $"{words[0][0]}{words[^1][0]}";

            rows.Add(new ShipmentRow
            {
                Shipper = name,
                Initials = initials,
                AvatarColor = avatarColors[i % avatarColors.Length],
                Tracking = $"NB-{482130 + i * 377:D6}",
                Lane = lanes[i % lanes.Length],
                Status = status.Text,
                StatusColor = status.Color,
                Eta = $"ETA {(i % 12) + 1}:{(i * 13) % 60:D2} {(i % 2 == 0 ? "PM" : "AM")}",
                Weight = $"{12 + (i * 17) % 31},{(i * 241) % 1000:D3} lb",
                Pallets = $"{6 + (i * 5) % 20} pallets",
            });
        }

        Shipments = rows;
    }
}

public sealed class ShipmentRow
{
    public required string Shipper { get; init; }
    public required string Initials { get; init; }
    public required Color AvatarColor { get; init; }
    public required string Tracking { get; init; }
    public required string Lane { get; init; }
    public required string Status { get; init; }
    public required Color StatusColor { get; init; }
    public required string Eta { get; init; }
    public required string Weight { get; init; }
    public required string Pallets { get; init; }
}
