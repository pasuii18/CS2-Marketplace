using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Core.Models.UsersModels;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<UsersPictures> UsersPictures => Set<UsersPictures>();

        public DbSet<Deals> Deals => Set<Deals>();
        public DbSet<DealOffers> DealOffers => Set<DealOffers>();
        public DbSet<DealsHistory> DealsHistory => Set<DealsHistory>();

        public DbSet<Items> Items => Set<Items>();
        public DbSet<ItemsPictures> ItemsPictures => Set<ItemsPictures>();
        public DbSet<Skins> Skins => Set<Skins>();

        public DbSet<ItemsType> ItemsType => Set<ItemsType>();
        public DbSet<ItemsTypeCharacteristics> ItemsTypeCharacteristics => Set<ItemsTypeCharacteristics>();
        public DbSet<TypeCharacteristics> TypeCharacteristics => Set<TypeCharacteristics>();

        public DbSet<ItemsClass> ItemsClass => Set<ItemsClass>();
        public DbSet<ItemsClassCharacteristics> ItemsClassCharacteristics => Set<ItemsClassCharacteristics>();
        public DbSet<ClassCharacteristics> ClassCharacteristics => Set<ClassCharacteristics>();

        public DbSet<ItemsSubClass> ItemsSubClass => Set<ItemsSubClass>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(user => user.IdUser);
                entity.Property(user => user.IdUserProfilePicture).IsRequired(false);
                entity.Property(user => user.UUID).IsRequired().HasMaxLength(10);
                entity.Property(user => user.Username).IsRequired().HasMaxLength(30);
                entity.HasIndex(user => user.Login).IsUnique();
                entity.Property(user => user.Login).IsRequired().HasMaxLength(20);
                entity.Property(user => user.Password).IsRequired().HasMaxLength(200);
                entity.Property(user => user.Description).IsRequired().HasMaxLength(190);

                entity.HasMany(user => user.Deals)
                    .WithOne(deal => deal.User)
                    .HasForeignKey(deal => deal.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(user => user.DealOffers)
                    .WithOne(dealOffer => dealOffer.Offerer)
                    .HasForeignKey(dealOffer => dealOffer.IdOfferer)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(user => user.UserProfilePicture)
                    .WithOne(userPFP => userPFP.User)
                    .HasForeignKey<UsersPictures>(userPFP => userPFP.IdUserProfilePicture)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(user => user.Role)
                    .WithMany(roles => roles.Users)
                    .HasForeignKey(roles => roles.IdRole)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<UsersPictures>(entity =>
            {
                entity.HasKey(userPFP => userPFP.IdUserProfilePicture);
                entity.Property(userPFP => userPFP.PicturePath).IsRequired();

                entity.HasOne(userPFP => userPFP.User)
                .WithOne(user => user.UserProfilePicture)
                .HasForeignKey<Users>(user => user.IdUserProfilePicture)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(role => role.IdRole);
                entity.HasIndex(role => role.RoleName).IsUnique();

                entity.HasMany(role => role.Users)
                .WithOne(users => users.Role)
                .HasForeignKey(users => users.IdRole)
                .OnDelete(DeleteBehavior.NoAction);
            });


            // ITEMS
            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(item => item.IdItem);
                entity.Property(item => item.IdItemPicture).IsRequired(false);
                entity.Property(item => item.IdItemType).IsRequired();
                entity.Property(item => item.IdSkin).IsRequired();
                entity.Property(item => item.Rarity).IsRequired().HasMaxLength(20);

                entity.HasMany(item => item.Deals)
                    .WithOne(deal => deal.Item)
                    .HasForeignKey(deal => deal.IdItem)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(item => item.ItemPicture)
                    .WithOne(itemPicture => itemPicture.Item)
                    .HasForeignKey<ItemsPictures>(itemPicture => itemPicture.IdItemPicture)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(item => item.Skin)
                    .WithMany(skins => skins.Items)
                    .HasForeignKey(skins => skins.IdSkin)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(item => item.ItemsType)
                    .WithMany(itemType => itemType.Items)
                    .HasForeignKey(itemType => itemType.IdItemType)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<ItemsPictures>(entity =>
            {
                entity.HasKey(itemPicture => itemPicture.IdItemPicture);
                entity.HasIndex(itemPicture => itemPicture.ItemPicturePath).IsUnique();

                entity.HasOne(itemPicture => itemPicture.Item)
                    .WithOne(items => items.ItemPicture)
                    .HasForeignKey<Items>(items => items.IdItemPicture)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Skins>(entity =>
            {
                entity.HasKey(skins => skins.IdSkin);
                entity.Property(skins => skins.SkinName).IsRequired().HasMaxLength(50);
                entity.HasIndex(skins => skins.SkinName).IsUnique();

                entity.HasMany(skins => skins.Items)
                    .WithOne(items => items.Skin)
                    .HasForeignKey(items => items.IdSkin)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // DEALS
            modelBuilder.Entity<Deals>(entity =>
            {
                entity.HasKey(deal => deal.IdDeal);
                entity.Property(deal => deal.IdItem).IsRequired();
                entity.Property(user => user.IdUser).IsRequired();
                entity.Property(deal => deal.Price).IsRequired().HasColumnType("decimal").HasPrecision(10, 2);

                entity.HasOne(deal => deal.User)
                    .WithMany(user => user.Deals)
                    .HasForeignKey(deal => deal.IdUser)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(deal => deal.Item)
                    .WithMany(item => item.Deals)
                    .HasForeignKey(deal => deal.IdItem)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(deal => deal.DealOffers)
                    .WithOne(dealOffer => dealOffer.Deal)
                    .HasForeignKey(dealOffer => dealOffer.IdDeal)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<DealOffers>(entity =>
            {
                entity.HasKey(dealOffer => dealOffer.IdDealOffer);
                entity.Property(dealOffer => dealOffer.IdDeal).IsRequired();
                entity.Property(dealOffer => dealOffer.IdOfferer).IsRequired();
                entity.Property(dealOffer => dealOffer.OfferPrice).IsRequired().HasColumnType("decimal").HasPrecision(10, 2);

                entity.HasOne(dealOffer => dealOffer.Offerer)
                .WithMany(user => user.DealOffers)
                .HasForeignKey(buyer => buyer.IdOfferer);
                //.OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(dealOffer => dealOffer.Deal)
                .WithMany(deal => deal.DealOffers)
                .HasForeignKey(deal => deal.IdDeal);
                //.OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<DealsHistory>(entity =>
            {
                entity.HasKey(dealsHistory => dealsHistory.IdDealsHistory);
                entity.Property(dealsHistory => dealsHistory.IdItem).IsRequired();
                entity.Property(dealsHistory => dealsHistory.IdSeller).IsRequired();
                entity.Property(dealsHistory => dealsHistory.IdBuyer).IsRequired();
                entity.Property(dealsHistory => dealsHistory.Price).IsRequired().HasColumnType("decimal").HasPrecision(10, 2);
                entity.Property(dealsHistory => dealsHistory.Date).IsRequired();

                entity.HasOne(dealsHistory => dealsHistory.Item)
                .WithMany(item => item.DealsHistory)
                .HasForeignKey(dealsHistory => dealsHistory.IdItem)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(dealsHistory => dealsHistory.Seller)
                .WithMany(user => user.SellingHistory)
                .HasForeignKey(dealsHistory => dealsHistory.IdSeller)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(dealsHistory => dealsHistory.Buyer)
               .WithMany(user => user.BuyingHistory)
               .HasForeignKey(dealsHistory => dealsHistory.IdBuyer)
               .OnDelete(DeleteBehavior.NoAction);
            });

            // ITEM TYPE LEVEL
            modelBuilder.Entity<ItemsType>(entity =>
            {
                entity.HasKey(itemsType => itemsType.IdItemType);
                entity.HasIndex(itemsType => itemsType.IdItemClass).IsUnique();
                entity.Property(itemsType => itemsType.ItemTypeName).IsRequired().HasMaxLength(50);

                entity.HasMany(itemsType => itemsType.Items)
                .WithOne(items => items.ItemsType)
                .HasForeignKey(items => items.IdItemType)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(itemsType => itemsType.ItemsClass)
                .WithMany(itemsClass => itemsClass.ItemsTypes)
                .HasForeignKey(itemsClass => itemsClass.IdItemClass)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(itemsType => itemsType.ItemsTypeCharacteristics)
                .WithOne(itemsTypeCharacteristics => itemsTypeCharacteristics.ItemType)
                .HasForeignKey(itemsTypeCharacteristics => itemsTypeCharacteristics.IdItemType)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<TypeCharacteristics>(entity =>
            {
                entity.HasKey(typeCharacteristics => typeCharacteristics.IdTypeCharacteristic);
                entity.HasIndex(typeCharacteristics => typeCharacteristics.TypeCharacteristicName).IsUnique();

                entity.HasMany(typeCharacteristics => typeCharacteristics.ItemsTypeCharacteristics)
                .WithOne(itemsTypeCharacteristics => itemsTypeCharacteristics.TypeCharacteristic)
                .HasForeignKey(itemsTypeCharacteristics => itemsTypeCharacteristics.IdTypeCharacteristic)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ItemsTypeCharacteristics>(entity =>
            {
                entity.HasKey(itemsTypeCharacteristics => new
                {
                    itemsTypeCharacteristics.IdItemType,
                    itemsTypeCharacteristics.IdTypeCharacteristic
                });
                entity.Property(itemsTypeCharacteristics => itemsTypeCharacteristics.TypeCharacteristicValue).IsRequired().HasMaxLength(50).HasColumnType("varchar");

                entity.HasOne(itemsTypeCharacteristics => itemsTypeCharacteristics.ItemType)
                    .WithMany(itemsType => itemsType.ItemsTypeCharacteristics)
                    .HasForeignKey(itemsTypeCharacteristics => itemsTypeCharacteristics.IdItemType)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(itemsTypeCharacteristics => itemsTypeCharacteristics.TypeCharacteristic)
                    .WithMany(typeCharacteristics => typeCharacteristics.ItemsTypeCharacteristics)
                    .HasForeignKey(itemsTypeCharacteristics => itemsTypeCharacteristics.IdTypeCharacteristic)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // ITEM TYPE CLASS LEVEL
            modelBuilder.Entity<ItemsClass>(entity =>
            {
                entity.HasKey(itemsClass => itemsClass.IdItemClass);
                entity.HasIndex(itemsClass => itemsClass.IdItemSubClass).IsUnique();
                entity.Property(itemsClass => itemsClass.ItemClassName).IsRequired().HasMaxLength(50);

                entity.HasMany(itemsClass => itemsClass.ItemsTypes)
                .WithOne(itemsType => itemsType.ItemsClass)
                .HasForeignKey(itemsType => itemsType.IdItemClass)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(itemsClass => itemsClass.ItemsSubClass)
                .WithMany(itemsSubClass => itemsSubClass.ItemsClasses)
                .HasForeignKey(itemsSubClass => itemsSubClass.IdItemSubClass)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(itemsClass => itemsClass.ItemsClassCharacteristics)
                .WithOne(itemsClassCharacteristics => itemsClassCharacteristics.ItemClass)
                .HasForeignKey(itemsClassCharacteristics => itemsClassCharacteristics.IdItemClass)
                .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<ClassCharacteristics>(entity =>
            {
                entity.HasKey(classCharacteristics => classCharacteristics.IdClassCharacteristic);
                entity.HasIndex(classCharacteristics => classCharacteristics.ClassCharacteristicName).IsUnique();

                entity.HasMany(classCharacteristics => classCharacteristics.ItemsClassCharacteristics)
                .WithOne(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                .HasForeignKey(itemsClassCharacteristics => itemsClassCharacteristics.IdClassCharacteristic)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ItemsClassCharacteristics>(entity =>
            {
                entity.HasKey(itemsClassCharacteristics => new
                {
                    itemsClassCharacteristics.IdItemClass,
                    itemsClassCharacteristics.IdClassCharacteristic
                });
                entity.Property(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristicValue).IsRequired().HasMaxLength(50).HasColumnType("varchar");

                entity.HasOne(itemsClassCharacteristics => itemsClassCharacteristics.ItemClass)
                    .WithMany(itemsClass => itemsClass.ItemsClassCharacteristics)
                    .HasForeignKey(itemsClassCharacteristics => itemsClassCharacteristics.IdItemClass)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                    .WithMany(classCharacteristics => classCharacteristics.ItemsClassCharacteristics)
                    .HasForeignKey(itemsClassCharacteristics => itemsClassCharacteristics.IdClassCharacteristic)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // ITEM TYPE SUBCLASS LEVEL
            modelBuilder.Entity<ItemsSubClass>(entity =>
            {
                entity.HasKey(itemsSubClass => itemsSubClass.IdItemSubClass);
                entity.HasIndex(itemsSubClass => itemsSubClass.ItemSubClassName).IsUnique();

                entity.HasMany(itemsSubClass => itemsSubClass.ItemsClasses)
                    .WithOne(itemsClass => itemsClass.ItemsSubClass)
                    .HasForeignKey(itemsClass => itemsClass.IdItemSubClass)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ITEMSUBCLASS VALUES
            AddDbRows<ItemsSubClass>(modelBuilder,
                new ItemsSubClass { IdItemSubClass = 1, ItemSubClassName = "Glock-18" },
                new ItemsSubClass { IdItemSubClass = 2, ItemSubClassName = "P2000" },
                new ItemsSubClass { IdItemSubClass = 3, ItemSubClassName = "USP-S" },
                new ItemsSubClass { IdItemSubClass = 4, ItemSubClassName = "Dual Berettas" },
                new ItemsSubClass { IdItemSubClass = 5, ItemSubClassName = "P250" },
                new ItemsSubClass { IdItemSubClass = 6, ItemSubClassName = "Tec-9" },
                new ItemsSubClass { IdItemSubClass = 7, ItemSubClassName = "Five-SeveN" },
                new ItemsSubClass { IdItemSubClass = 8, ItemSubClassName = "CZ75-Auto" },
                new ItemsSubClass { IdItemSubClass = 9, ItemSubClassName = "Desert Eagle" },
                new ItemsSubClass { IdItemSubClass = 10, ItemSubClassName = "R8 Revolver" },
                new ItemsSubClass { IdItemSubClass = 11, ItemSubClassName = "MAC-10" },
                new ItemsSubClass { IdItemSubClass = 12, ItemSubClassName = "MP9" },
                new ItemsSubClass { IdItemSubClass = 13, ItemSubClassName = "MP7" },
                new ItemsSubClass { IdItemSubClass = 14, ItemSubClassName = "MP5-SD" },
                new ItemsSubClass { IdItemSubClass = 15, ItemSubClassName = "UMP-45" },
                new ItemsSubClass { IdItemSubClass = 16, ItemSubClassName = "P90" },
                new ItemsSubClass { IdItemSubClass = 17, ItemSubClassName = "PP-BIZON" },
                new ItemsSubClass { IdItemSubClass = 18, ItemSubClassName = "Nova" },
                new ItemsSubClass { IdItemSubClass = 19, ItemSubClassName = "XM1014" },
                new ItemsSubClass { IdItemSubClass = 20, ItemSubClassName = "MAG-7" },
                new ItemsSubClass { IdItemSubClass = 21, ItemSubClassName = "Sawed-Off" },
                new ItemsSubClass { IdItemSubClass = 22, ItemSubClassName = "Negev" },
                new ItemsSubClass { IdItemSubClass = 23, ItemSubClassName = "M249" },
                new ItemsSubClass { IdItemSubClass = 24, ItemSubClassName = "Galil AR" },
                new ItemsSubClass { IdItemSubClass = 25, ItemSubClassName = "FAMAS" },
                new ItemsSubClass { IdItemSubClass = 26, ItemSubClassName = "AK-47" },
                new ItemsSubClass { IdItemSubClass = 27, ItemSubClassName = "M4A1-S" },
                new ItemsSubClass { IdItemSubClass = 28, ItemSubClassName = "M4A4" },
                new ItemsSubClass { IdItemSubClass = 29, ItemSubClassName = "SG 553" },
                new ItemsSubClass { IdItemSubClass = 30, ItemSubClassName = "AUG" },
                new ItemsSubClass { IdItemSubClass = 31, ItemSubClassName = "SSG 08" },
                new ItemsSubClass { IdItemSubClass = 32, ItemSubClassName = "AWP" },
                new ItemsSubClass { IdItemSubClass = 33, ItemSubClassName = "G3SG1" },
                new ItemsSubClass { IdItemSubClass = 34, ItemSubClassName = "SCAR-20" },
                new ItemsSubClass { IdItemSubClass = 35, ItemSubClassName = "Bayonet" },
                new ItemsSubClass { IdItemSubClass = 36, ItemSubClassName = "Bowie" },
                new ItemsSubClass { IdItemSubClass = 37, ItemSubClassName = "Butterfly" },
                new ItemsSubClass { IdItemSubClass = 38, ItemSubClassName = "Classic" },
                new ItemsSubClass { IdItemSubClass = 39, ItemSubClassName = "Falshion" },
                new ItemsSubClass { IdItemSubClass = 40, ItemSubClassName = "Flip" },
                new ItemsSubClass { IdItemSubClass = 41, ItemSubClassName = "Gut" },
                new ItemsSubClass { IdItemSubClass = 42, ItemSubClassName = "Huntsman" },
                new ItemsSubClass { IdItemSubClass = 43, ItemSubClassName = "Karambit" },
                new ItemsSubClass { IdItemSubClass = 44, ItemSubClassName = "M9" },
                new ItemsSubClass { IdItemSubClass = 45, ItemSubClassName = "Navaja" },
                new ItemsSubClass { IdItemSubClass = 46, ItemSubClassName = "Nomad" },
                new ItemsSubClass { IdItemSubClass = 47, ItemSubClassName = "Paracord" },
                new ItemsSubClass { IdItemSubClass = 48, ItemSubClassName = "Shadow Daggers" },
                new ItemsSubClass { IdItemSubClass = 49, ItemSubClassName = "Skeleton" },
                new ItemsSubClass { IdItemSubClass = 50, ItemSubClassName = "Stiletto" },
                new ItemsSubClass { IdItemSubClass = 51, ItemSubClassName = "Survival" },
                new ItemsSubClass { IdItemSubClass = 52, ItemSubClassName = "Talon" },
                new ItemsSubClass { IdItemSubClass = 53, ItemSubClassName = "Ursus" },
                new ItemsSubClass { IdItemSubClass = 54, ItemSubClassName = "Hand Wraps" },
                new ItemsSubClass { IdItemSubClass = 55, ItemSubClassName = "Moto" },
                new ItemsSubClass { IdItemSubClass = 56, ItemSubClassName = "Specialist" },
                new ItemsSubClass { IdItemSubClass = 57, ItemSubClassName = "Sport" },
                new ItemsSubClass { IdItemSubClass = 58, ItemSubClassName = "Bloodhound" },
                new ItemsSubClass { IdItemSubClass = 59, ItemSubClassName = "Hydra" },
                new ItemsSubClass { IdItemSubClass = 60, ItemSubClassName = "Broken Fang" },
                new ItemsSubClass { IdItemSubClass = 61, ItemSubClassName = "Driver" },
                new ItemsSubClass { IdItemSubClass = 62, ItemSubClassName = "Music Kit" },
                new ItemsSubClass { IdItemSubClass = 63, ItemSubClassName = "Case" }
            );

            // CLASS LEVEL
            AddDbRows<ItemsClass>(modelBuilder,
            new ItemsClass { IdItemClass = 1, IdItemSubClass = 63, ItemClassName = "Case" },
            new ItemsClass { IdItemClass = 2, IdItemSubClass = 9, ItemClassName = "Pistol" },
            new ItemsClass { IdItemClass = 3, IdItemSubClass = 27, ItemClassName = "Rifle" },
            new ItemsClass { IdItemClass = 4, IdItemSubClass = 62, ItemClassName = "Music Kit" }
            );

            AddDbRows<ClassCharacteristics>(modelBuilder,
            new ClassCharacteristics { IdClassCharacteristic = 1, ClassCharacteristicName = "Fire Range" },
            new ClassCharacteristics { IdClassCharacteristic = 2, ClassCharacteristicName = "Optical Scope" },
            new ClassCharacteristics { IdClassCharacteristic = 3, ClassCharacteristicName = "Magazine Capacity" },
            new ClassCharacteristics { IdClassCharacteristic = 4, ClassCharacteristicName = "Fire Rate" },
            new ClassCharacteristics { IdClassCharacteristic = 5, ClassCharacteristicName = "Blade Length" },
            new ClassCharacteristics { IdClassCharacteristic = 6, ClassCharacteristicName = "Material" },
            new ClassCharacteristics { IdClassCharacteristic = 7, ClassCharacteristicName = "Patch Place" },
            new ClassCharacteristics { IdClassCharacteristic = 8, ClassCharacteristicName = "Agent Game Side" }
            );

            AddDbRows<ItemsClassCharacteristics>(modelBuilder,
            new ItemsClassCharacteristics { IdItemClass = 2, IdClassCharacteristic = 1, ClassCharacteristicValue = "1800m" },
            new ItemsClassCharacteristics { IdItemClass = 2, IdClassCharacteristic = 2, ClassCharacteristicValue = "No" },
            new ItemsClassCharacteristics { IdItemClass = 2, IdClassCharacteristic = 3, ClassCharacteristicValue = "7 bullets" },
            new ItemsClassCharacteristics { IdItemClass = 3, IdClassCharacteristic = 4, ClassCharacteristicValue = "3bps" },
            new ItemsClassCharacteristics { IdItemClass = 3, IdClassCharacteristic = 3, ClassCharacteristicValue = "30 bullets" }
            );

            // TYPE LEVEL
            AddDbRows<ItemsType>(modelBuilder,
            new ItemsType { IdItemType = 1, IdItemClass = 1, ItemTypeName = "Lootboxes" },
            new ItemsType { IdItemType = 2, IdItemClass = 2, ItemTypeName = "Weapon" },
            new ItemsType { IdItemType = 3, IdItemClass = 3, ItemTypeName = "Weapon" },
            new ItemsType { IdItemType = 4, IdItemClass = 4, ItemTypeName = "Accessories" }
            );

            AddDbRows<TypeCharacteristics>(modelBuilder,
            new TypeCharacteristics { IdTypeCharacteristic = 1, TypeCharacteristicName = "Release Year" },
            new TypeCharacteristics { IdTypeCharacteristic = 2, TypeCharacteristicName = "Release Season" },
            new TypeCharacteristics { IdTypeCharacteristic = 3, TypeCharacteristicName = "Stat-Track" }
            );

            AddDbRows<ItemsTypeCharacteristics>(modelBuilder,
            new ItemsTypeCharacteristics { IdItemType = 1, IdTypeCharacteristic = 1, TypeCharacteristicValue = "2023" },
            new ItemsTypeCharacteristics { IdItemType = 1, IdTypeCharacteristic = 2, TypeCharacteristicValue = "Spring" },
            new ItemsTypeCharacteristics { IdItemType = 3, IdTypeCharacteristic = 3, TypeCharacteristicValue = "Yes" },
            new ItemsTypeCharacteristics { IdItemType = 4, IdTypeCharacteristic = 1, TypeCharacteristicValue = "2024" }
            );

            AddDbRows<ItemsPictures>(modelBuilder,
            new ItemsPictures { IdItemPicture = 1, ItemPicturePath = "images\\ItemsPictures\\case.png" },
            new ItemsPictures { IdItemPicture = 2, ItemPicturePath = "images\\ItemsPictures\\DesertEagle.png" },
            new ItemsPictures { IdItemPicture = 3, ItemPicturePath = "images\\ItemsPictures\\M4A1-S.png" },
            new ItemsPictures { IdItemPicture = 4, ItemPicturePath = "images\\ItemsPictures\\MusicKit.png" }
            );

            AddDbRows<Skins>(modelBuilder,
            new Skins { IdSkin = 1, SkinName = "SnakeBite" },
            new Skins { IdSkin = 2, SkinName = "Strealkovaya" },
            new Skins { IdSkin = 3, SkinName = "Forest Boreal" },
            new Skins { IdSkin = 4, SkinName = "ISOxo - inhuman" }
            );

            // ITEM LEVEL
            AddDbRows<Items>(modelBuilder,
            new Items { IdItem = 1, IdItemType = 1, IdItemPicture = 1, IdSkin = 1, Rarity = 2 },
            new Items { IdItem = 2, IdItemType = 2, IdItemPicture = 2, IdSkin = 2, Rarity = 3 },
            new Items { IdItem = 3, IdItemType = 3, IdItemPicture = 3, IdSkin = 3, Rarity = 1 },
            new Items { IdItem = 4, IdItemType = 4, IdItemPicture = 4, IdSkin = 4, Rarity = 2 }
            );

            AddDbRows<Deals>(modelBuilder,
            new Deals { IdDeal = 1, IdItem = 1, IdUser = 1, Price = 0.36m},
            new Deals { IdDeal = 2, IdItem = 2, IdUser = 2, Price = 2.02m},
            new Deals { IdDeal = 3, IdItem = 3, IdUser = 3, Price = 0.22m},
            new Deals { IdDeal = 4, IdItem = 4, IdUser = 4, Price = 4.67m},
            new Deals { IdDeal = 5, IdItem = 1, IdUser = 2, Price = 0.42m},
            new Deals { IdDeal = 6, IdItem = 2, IdUser = 3, Price = 1.97m},
            new Deals { IdDeal = 7, IdItem = 3, IdUser = 4, Price = 0.23m},
            new Deals { IdDeal = 8, IdItem = 4, IdUser = 1, Price = 4.80m}
            );

            AddDbRows<Roles>(modelBuilder,
            new Roles { IdRole = 1, RoleName = "Member" },
            new Roles { IdRole = 2, RoleName = "Developer" },
            new Roles { IdRole = 3, RoleName = "Administrator" }
            );

            AddDbRows<UsersPictures>(modelBuilder,
            new UsersPictures { IdUserProfilePicture = 1, PicturePath = "images\\UsersProfilePictures\\Arseny.jpg" },
            new UsersPictures { IdUserProfilePicture = 2, PicturePath = "images\\UsersProfilePictures\\Roma.jpg" },
            new UsersPictures { IdUserProfilePicture = 3, PicturePath = "images\\UsersProfilePictures\\Kirill.jpg" },
            new UsersPictures { IdUserProfilePicture = 4, PicturePath = "images\\UsersProfilePictures\\Serega.jpg" }
            );

            AddDbRows<Users>(modelBuilder,
            new Users { IdUser = 1, IdUserProfilePicture = 1, UUID = "1", Username = "Arseny", Login = "arseny", Password = "123", Description = "Hi! My name is Arseny Trubilo." },
            new Users { IdUser = 2, IdUserProfilePicture = 2, UUID = "2", Username = "Roma", Login = "roma", Password = "123", Description = "Certified Boykisser" },
            new Users { IdUser = 3, IdUserProfilePicture = 3, IdRole = 3, UUID = "3", Username = "Kirim", Login = "kirill", Password = "123", Description = "Мир - картофелина" },
            new Users { IdUser = 4, IdUserProfilePicture = 4, UUID = "4", Username = "Серега Крутой 78рус", Login = "serega", Password = "123", Description = "<h2>HELLO??????</h2>" }
            );

            AddDbRows<DealOffers>(modelBuilder,
            new DealOffers { IdDealOffer = 1, IdDeal = 1, IdOfferer = 4, OfferPrice = 0.34m },
            new DealOffers { IdDealOffer = 2, IdDeal = 2, IdOfferer = 3, OfferPrice = 1.80m },
            new DealOffers { IdDealOffer = 3, IdDeal = 3, IdOfferer = 2, OfferPrice = 0.18m },
            new DealOffers { IdDealOffer = 4, IdDeal = 4, IdOfferer = 1, OfferPrice = 4.23m },
            new DealOffers { IdDealOffer = 5, IdDeal = 1, IdOfferer = 3, OfferPrice = 0.35m },
            new DealOffers { IdDealOffer = 6, IdDeal = 2, IdOfferer = 1, OfferPrice = 1.81m },
            new DealOffers { IdDealOffer = 7, IdDeal = 3, IdOfferer = 4, OfferPrice = 0.19m },
            new DealOffers { IdDealOffer = 8, IdDeal = 4, IdOfferer = 2, OfferPrice = 4.24m }
            );

            AddDbRows<DealsHistory>(modelBuilder,
            new DealsHistory { IdDealsHistory = 1, IdItem = 1, IdBuyer = 1, IdSeller = 3, Price = 0.37m, Date = DateTime.Now.AddHours(-24) },
            new DealsHistory { IdDealsHistory = 2, IdItem = 1, IdBuyer = 2, IdSeller = 1, Price = 0.34m, Date = DateTime.Now.AddHours(-40) },
            new DealsHistory { IdDealsHistory = 3, IdItem = 2, IdBuyer = 4, IdSeller = 2, Price = 2.01m, Date = DateTime.Now.AddHours(-20) },
            new DealsHistory { IdDealsHistory = 4, IdItem = 2, IdBuyer = 3, IdSeller = 4, Price = 2.13m, Date = DateTime.Now.AddHours(-45) },
            new DealsHistory { IdDealsHistory = 5, IdItem = 3, IdBuyer = 1, IdSeller = 2, Price = 0.23m, Date = DateTime.Now.AddHours(-60) },
            new DealsHistory { IdDealsHistory = 6, IdItem = 3, IdBuyer = 2, IdSeller = 4, Price = 0.20m, Date = DateTime.Now.AddHours(-6) },
            new DealsHistory { IdDealsHistory = 7, IdItem = 4, IdBuyer = 3, IdSeller = 1, Price = 4.98m, Date = DateTime.Now.AddHours(-24) },
            new DealsHistory { IdDealsHistory = 8, IdItem = 4, IdBuyer = 4, IdSeller = 3, Price = 5.02m, Date = DateTime.Now.AddHours(-80) }
            );
        }
        private static void AddDbRows<T>(ModelBuilder modelBuilder, params T[] entities) where T : class
        {
            modelBuilder.Entity<T>().HasData(entities);
        }
    }
}