using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSMarketApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassCharacteristics",
                columns: table => new
                {
                    IdClassCharacteristic = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassCharacteristicName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCharacteristics", x => x.IdClassCharacteristic);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPictures",
                columns: table => new
                {
                    IdItemPicture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemPicturePath = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPictures", x => x.IdItemPicture);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSubClass",
                columns: table => new
                {
                    IdItemSubClass = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemSubClassName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSubClass", x => x.IdItemSubClass);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Skins",
                columns: table => new
                {
                    IdSkin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkinName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skins", x => x.IdSkin);
                });

            migrationBuilder.CreateTable(
                name: "TypeCharacteristics",
                columns: table => new
                {
                    IdTypeCharacteristic = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCharacteristicName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCharacteristics", x => x.IdTypeCharacteristic);
                });

            migrationBuilder.CreateTable(
                name: "UsersPictures",
                columns: table => new
                {
                    IdUserProfilePicture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPictures", x => x.IdUserProfilePicture);
                });

            migrationBuilder.CreateTable(
                name: "ItemsClass",
                columns: table => new
                {
                    IdItemClass = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdItemSubClass = table.Column<int>(type: "int", nullable: false),
                    ItemClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsClass", x => x.IdItemClass);
                    table.ForeignKey(
                        name: "FK_ItemsClass_ItemsSubClass_IdItemSubClass",
                        column: x => x.IdItemSubClass,
                        principalTable: "ItemsSubClass",
                        principalColumn: "IdItemSubClass",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUserProfilePicture = table.Column<int>(type: "int", nullable: true),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    UUID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(190)", maxLength: 190, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole");
                    table.ForeignKey(
                        name: "FK_Users_UsersPictures_IdUserProfilePicture",
                        column: x => x.IdUserProfilePicture,
                        principalTable: "UsersPictures",
                        principalColumn: "IdUserProfilePicture");
                });

            migrationBuilder.CreateTable(
                name: "ItemsClassCharacteristics",
                columns: table => new
                {
                    IdItemClass = table.Column<int>(type: "int", nullable: false),
                    IdClassCharacteristic = table.Column<int>(type: "int", nullable: false),
                    ClassCharacteristicValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsClassCharacteristics", x => new { x.IdItemClass, x.IdClassCharacteristic });
                    table.ForeignKey(
                        name: "FK_ItemsClassCharacteristics_ClassCharacteristics_IdClassCharacteristic",
                        column: x => x.IdClassCharacteristic,
                        principalTable: "ClassCharacteristics",
                        principalColumn: "IdClassCharacteristic");
                    table.ForeignKey(
                        name: "FK_ItemsClassCharacteristics_ItemsClass_IdItemClass",
                        column: x => x.IdItemClass,
                        principalTable: "ItemsClass",
                        principalColumn: "IdItemClass");
                });

            migrationBuilder.CreateTable(
                name: "ItemsType",
                columns: table => new
                {
                    IdItemType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdItemClass = table.Column<int>(type: "int", nullable: false),
                    ItemTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsType", x => x.IdItemType);
                    table.ForeignKey(
                        name: "FK_ItemsType_ItemsClass_IdItemClass",
                        column: x => x.IdItemClass,
                        principalTable: "ItemsClass",
                        principalColumn: "IdItemClass",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdItemPicture = table.Column<int>(type: "int", nullable: true),
                    IdSkin = table.Column<int>(type: "int", nullable: false),
                    IdItemType = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.IdItem);
                    table.ForeignKey(
                        name: "FK_Items_ItemsPictures_IdItemPicture",
                        column: x => x.IdItemPicture,
                        principalTable: "ItemsPictures",
                        principalColumn: "IdItemPicture");
                    table.ForeignKey(
                        name: "FK_Items_ItemsType_IdItemType",
                        column: x => x.IdItemType,
                        principalTable: "ItemsType",
                        principalColumn: "IdItemType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Skins_IdSkin",
                        column: x => x.IdSkin,
                        principalTable: "Skins",
                        principalColumn: "IdSkin");
                });

            migrationBuilder.CreateTable(
                name: "ItemsTypeCharacteristics",
                columns: table => new
                {
                    IdItemType = table.Column<int>(type: "int", nullable: false),
                    IdTypeCharacteristic = table.Column<int>(type: "int", nullable: false),
                    TypeCharacteristicValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsTypeCharacteristics", x => new { x.IdItemType, x.IdTypeCharacteristic });
                    table.ForeignKey(
                        name: "FK_ItemsTypeCharacteristics_ItemsType_IdItemType",
                        column: x => x.IdItemType,
                        principalTable: "ItemsType",
                        principalColumn: "IdItemType");
                    table.ForeignKey(
                        name: "FK_ItemsTypeCharacteristics_TypeCharacteristics_IdTypeCharacteristic",
                        column: x => x.IdTypeCharacteristic,
                        principalTable: "TypeCharacteristics",
                        principalColumn: "IdTypeCharacteristic");
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    IdDeal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdItem = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.IdDeal);
                    table.ForeignKey(
                        name: "FK_Deals_Items_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Items",
                        principalColumn: "IdItem");
                    table.ForeignKey(
                        name: "FK_Deals_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "DealsHistory",
                columns: table => new
                {
                    IdDealsHistory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdItem = table.Column<int>(type: "int", nullable: false),
                    IdSeller = table.Column<int>(type: "int", nullable: false),
                    IdBuyer = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealsHistory", x => x.IdDealsHistory);
                    table.ForeignKey(
                        name: "FK_DealsHistory_Items_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Items",
                        principalColumn: "IdItem");
                    table.ForeignKey(
                        name: "FK_DealsHistory_Users_IdBuyer",
                        column: x => x.IdBuyer,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_DealsHistory_Users_IdSeller",
                        column: x => x.IdSeller,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "DealOffers",
                columns: table => new
                {
                    IdDealOffer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDeal = table.Column<int>(type: "int", nullable: false),
                    IdOfferer = table.Column<int>(type: "int", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealOffers", x => x.IdDealOffer);
                    table.ForeignKey(
                        name: "FK_DealOffers_Deals_IdDeal",
                        column: x => x.IdDeal,
                        principalTable: "Deals",
                        principalColumn: "IdDeal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealOffers_Users_IdOfferer",
                        column: x => x.IdOfferer,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClassCharacteristics",
                columns: new[] { "IdClassCharacteristic", "ClassCharacteristicName" },
                values: new object[,]
                {
                    { 1, "Fire Range" },
                    { 2, "Optical Scope" },
                    { 3, "Magazine Capacity" },
                    { 4, "Fire Rate" },
                    { 5, "Blade Length" },
                    { 6, "Material" },
                    { 7, "Patch Place" },
                    { 8, "Agent Game Side" }
                });

            migrationBuilder.InsertData(
                table: "ItemsPictures",
                columns: new[] { "IdItemPicture", "ItemPicturePath" },
                values: new object[,]
                {
                    { 1, "images\\ItemsPictures\\case.png" },
                    { 2, "images\\ItemsPictures\\DesertEagle.png" },
                    { 3, "images\\ItemsPictures\\M4A1-S.png" },
                    { 4, "images\\ItemsPictures\\MusicKit.png" }
                });

            migrationBuilder.InsertData(
                table: "ItemsSubClass",
                columns: new[] { "IdItemSubClass", "ItemSubClassName" },
                values: new object[,]
                {
                    { 1, "Glock-18" },
                    { 2, "P2000" },
                    { 3, "USP-S" },
                    { 4, "Dual Berettas" },
                    { 5, "P250" },
                    { 6, "Tec-9" },
                    { 7, "Five-SeveN" },
                    { 8, "CZ75-Auto" },
                    { 9, "Desert Eagle" },
                    { 10, "R8 Revolver" },
                    { 11, "MAC-10" },
                    { 12, "MP9" },
                    { 13, "MP7" },
                    { 14, "MP5-SD" },
                    { 15, "UMP-45" },
                    { 16, "P90" },
                    { 17, "PP-BIZON" },
                    { 18, "Nova" },
                    { 19, "XM1014" },
                    { 20, "MAG-7" },
                    { 21, "Sawed-Off" },
                    { 22, "Negev" },
                    { 23, "M249" },
                    { 24, "Galil AR" },
                    { 25, "FAMAS" },
                    { 26, "AK-47" },
                    { 27, "M4A1-S" },
                    { 28, "M4A4" },
                    { 29, "SG 553" },
                    { 30, "AUG" },
                    { 31, "SSG 08" },
                    { 32, "AWP" },
                    { 33, "G3SG1" },
                    { 34, "SCAR-20" },
                    { 35, "Bayonet" },
                    { 36, "Bowie" },
                    { 37, "Butterfly" },
                    { 38, "Classic" },
                    { 39, "Falshion" },
                    { 40, "Flip" },
                    { 41, "Gut" },
                    { 42, "Huntsman" },
                    { 43, "Karambit" },
                    { 44, "M9" },
                    { 45, "Navaja" },
                    { 46, "Nomad" },
                    { 47, "Paracord" },
                    { 48, "Shadow Daggers" },
                    { 49, "Skeleton" },
                    { 50, "Stiletto" },
                    { 51, "Survival" },
                    { 52, "Talon" },
                    { 53, "Ursus" },
                    { 54, "Hand Wraps" },
                    { 55, "Moto" },
                    { 56, "Specialist" },
                    { 57, "Sport" },
                    { 58, "Bloodhound" },
                    { 59, "Hydra" },
                    { 60, "Broken Fang" },
                    { 61, "Driver" },
                    { 62, "Music Kit" },
                    { 63, "Case" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRole", "RoleName" },
                values: new object[,]
                {
                    { 1, "Member" },
                    { 2, "Developer" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "IdSkin", "SkinName" },
                values: new object[,]
                {
                    { 1, "SnakeBite" },
                    { 2, "Strealkovaya" },
                    { 3, "Forest Boreal" },
                    { 4, "ISOxo - inhuman" }
                });

            migrationBuilder.InsertData(
                table: "TypeCharacteristics",
                columns: new[] { "IdTypeCharacteristic", "TypeCharacteristicName" },
                values: new object[,]
                {
                    { 1, "Release Year" },
                    { 2, "Release Season" },
                    { 3, "Stat-track" }
                });

            migrationBuilder.InsertData(
                table: "UsersPictures",
                columns: new[] { "IdUserProfilePicture", "PicturePath" },
                values: new object[,]
                {
                    { 1, "images\\UsersProfilePictures\\Arseny.jpg" },
                    { 2, "images\\UsersProfilePictures\\Roma.jpg" },
                    { 3, "images\\UsersProfilePictures\\Kirill.jpg" },
                    { 4, "images\\UsersProfilePictures\\Serega.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ItemsClass",
                columns: new[] { "IdItemClass", "IdItemSubClass", "ItemClassName" },
                values: new object[,]
                {
                    { 1, 63, "Case" },
                    { 2, 9, "Pistol" },
                    { 3, 27, "Rifle" },
                    { 4, 62, "Music Kit" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Description", "IdRole", "IdUserProfilePicture", "Login", "Password", "UUID", "Username" },
                values: new object[,]
                {
                    { 1, "Hi! My name is Arseny Trubilo.", 1, 1, "arseny", "123", "1", "Arseny" },
                    { 2, "Certified Boykisser", 1, 2, "roma", "123", "2", "Roma" },
                    { 3, "Мир - картофелина", 3, 3, "kirill", "123", "3", "Kirim" },
                    { 4, "<h2>HELLO??????</h2>", 1, 4, "serega", "123", "4", "Серега Крутой 78рус" }
                });

            migrationBuilder.InsertData(
                table: "ItemsClassCharacteristics",
                columns: new[] { "IdClassCharacteristic", "IdItemClass", "ClassCharacteristicValue" },
                values: new object[,]
                {
                    { 1, 2, "1800m" },
                    { 2, 2, "No" },
                    { 3, 2, "7 bullets" },
                    { 3, 3, "30 bullets" },
                    { 4, 3, "3bps" }
                });

            migrationBuilder.InsertData(
                table: "ItemsType",
                columns: new[] { "IdItemType", "IdItemClass", "ItemTypeName" },
                values: new object[,]
                {
                    { 1, 1, "Lootboxes" },
                    { 2, 2, "Weapon" },
                    { 3, 3, "Weapon" },
                    { 4, 4, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "IdItem", "IdItemPicture", "IdItemType", "IdSkin", "Rarity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 2 },
                    { 2, 2, 2, 2, 3 },
                    { 3, 3, 3, 3, 1 },
                    { 4, 4, 4, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "ItemsTypeCharacteristics",
                columns: new[] { "IdItemType", "IdTypeCharacteristic", "TypeCharacteristicValue" },
                values: new object[,]
                {
                    { 1, 1, "2023" },
                    { 1, 2, "Spring" },
                    { 3, 3, "Yes" },
                    { 4, 1, "2024" }
                });

            migrationBuilder.InsertData(
                table: "Deals",
                columns: new[] { "IdDeal", "IdItem", "IdUser", "Price" },
                values: new object[,]
                {
                    { 1, 1, 1, 0.36m },
                    { 2, 2, 2, 2.02m },
                    { 3, 3, 3, 0.22m },
                    { 4, 4, 4, 4.67m },
                    { 5, 1, 2, 0.42m },
                    { 6, 2, 3, 1.97m },
                    { 7, 3, 4, 0.23m },
                    { 8, 4, 1, 4.80m }
                });

            migrationBuilder.InsertData(
                table: "DealsHistory",
                columns: new[] { "IdDealsHistory", "Date", "IdBuyer", "IdItem", "IdSeller", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 3, 9, 17, 17, 584, DateTimeKind.Local).AddTicks(3702), 1, 1, 3, 0.37m },
                    { 2, new DateTime(2024, 4, 2, 17, 17, 17, 584, DateTimeKind.Local).AddTicks(3720), 2, 1, 1, 0.34m },
                    { 3, new DateTime(2024, 4, 3, 13, 17, 17, 584, DateTimeKind.Local).AddTicks(3722), 4, 2, 2, 2.01m },
                    { 4, new DateTime(2024, 4, 2, 12, 17, 17, 584, DateTimeKind.Local).AddTicks(3725), 3, 2, 4, 2.13m },
                    { 5, new DateTime(2024, 4, 1, 21, 17, 17, 584, DateTimeKind.Local).AddTicks(3726), 1, 3, 2, 0.23m },
                    { 6, new DateTime(2024, 4, 4, 3, 17, 17, 584, DateTimeKind.Local).AddTicks(3728), 2, 3, 4, 0.20m },
                    { 7, new DateTime(2024, 4, 3, 9, 17, 17, 584, DateTimeKind.Local).AddTicks(3730), 3, 4, 1, 4.98m },
                    { 8, new DateTime(2024, 4, 1, 1, 17, 17, 584, DateTimeKind.Local).AddTicks(3731), 4, 4, 3, 5.02m }
                });

            migrationBuilder.InsertData(
                table: "DealOffers",
                columns: new[] { "IdDealOffer", "IdDeal", "IdOfferer", "OfferPrice" },
                values: new object[,]
                {
                    { 1, 1, 4, 0.34m },
                    { 2, 2, 3, 1.80m },
                    { 3, 3, 2, 0.18m },
                    { 4, 4, 1, 4.23m },
                    { 5, 1, 3, 0.35m },
                    { 6, 2, 1, 1.81m },
                    { 7, 3, 4, 0.19m },
                    { 8, 4, 2, 4.24m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCharacteristics_ClassCharacteristicName",
                table: "ClassCharacteristics",
                column: "ClassCharacteristicName",
                unique: true,
                filter: "[ClassCharacteristicName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DealOffers_IdDeal",
                table: "DealOffers",
                column: "IdDeal");

            migrationBuilder.CreateIndex(
                name: "IX_DealOffers_IdOfferer",
                table: "DealOffers",
                column: "IdOfferer");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_IdItem",
                table: "Deals",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_IdUser",
                table: "Deals",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_DealsHistory_IdBuyer",
                table: "DealsHistory",
                column: "IdBuyer");

            migrationBuilder.CreateIndex(
                name: "IX_DealsHistory_IdItem",
                table: "DealsHistory",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_DealsHistory_IdSeller",
                table: "DealsHistory",
                column: "IdSeller");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdItemPicture",
                table: "Items",
                column: "IdItemPicture",
                unique: true,
                filter: "[IdItemPicture] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdItemType",
                table: "Items",
                column: "IdItemType");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdSkin",
                table: "Items",
                column: "IdSkin");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsClass_IdItemSubClass",
                table: "ItemsClass",
                column: "IdItemSubClass",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsClassCharacteristics_IdClassCharacteristic",
                table: "ItemsClassCharacteristics",
                column: "IdClassCharacteristic");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPictures_ItemPicturePath",
                table: "ItemsPictures",
                column: "ItemPicturePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSubClass_ItemSubClassName",
                table: "ItemsSubClass",
                column: "ItemSubClassName",
                unique: true,
                filter: "[ItemSubClassName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsType_IdItemClass",
                table: "ItemsType",
                column: "IdItemClass",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsTypeCharacteristics_IdTypeCharacteristic",
                table: "ItemsTypeCharacteristics",
                column: "IdTypeCharacteristic");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true,
                filter: "[RoleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Skins_SkinName",
                table: "Skins",
                column: "SkinName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeCharacteristics_TypeCharacteristicName",
                table: "TypeCharacteristics",
                column: "TypeCharacteristicName",
                unique: true,
                filter: "[TypeCharacteristicName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdUserProfilePicture",
                table: "Users",
                column: "IdUserProfilePicture",
                unique: true,
                filter: "[IdUserProfilePicture] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealOffers");

            migrationBuilder.DropTable(
                name: "DealsHistory");

            migrationBuilder.DropTable(
                name: "ItemsClassCharacteristics");

            migrationBuilder.DropTable(
                name: "ItemsTypeCharacteristics");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "ClassCharacteristics");

            migrationBuilder.DropTable(
                name: "TypeCharacteristics");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ItemsPictures");

            migrationBuilder.DropTable(
                name: "ItemsType");

            migrationBuilder.DropTable(
                name: "Skins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UsersPictures");

            migrationBuilder.DropTable(
                name: "ItemsClass");

            migrationBuilder.DropTable(
                name: "ItemsSubClass");
        }
    }
}
