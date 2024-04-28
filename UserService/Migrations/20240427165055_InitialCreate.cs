using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aspnet_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    meta = table.Column<object>(type: "jsonb", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnet_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "aspnet_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_aspnet_user_claims_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_aspnet_user_logins_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_aspnet_user_roles_aspnet_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "aspnet_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_aspnet_user_roles_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnet_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aspnet_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_aspnet_user_tokens_aspnet_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_complete = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_tasks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "aspnet_users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "aspnet_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "meta", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { "002ddb66-64b5-4113-91d7-7a950b05587e", 0, "2141356cd6e5439ef174fd5d962fdab3f1b01da3", "Nathan_Schiller28@yahoo.com", true, false, null, null, "NATHAN_SCHILLER28@YAHOO.COM", "NATHAN_SCHILLER", "3fafe00a2c31ee5e8c6d43e050f9b4312e6fee1b", "(305) 263-5064 x2435", true, "4ef05ddd491db60f199e16fd91ef86fabb854da8", false, "Nathan_Schiller" },
                    { "0146144f-f8c1-4670-8110-71dd79cc51dd", 0, "19eae57ef981201848c3a90651e43eabc7c85646", "Alene22_Kunde@gmail.com", false, false, null, null, "ALENE22_KUNDE@GMAIL.COM", "ALENE22", "0843a91e1d0e470f2bb890a43c7eef24e29613d2", "1-220-378-8520", false, "b4b80abe031cd030df0d1302bcf2e9dfd9c45238", false, "Alene22" },
                    { "04bb2d8f-f3f7-441e-b6d3-f5076d6594ec", 0, "180d447a72b9dfcfdad3d847a18e03f8ab71ffcc", "Kara2881@gmail.com", false, false, null, null, "KARA2881@GMAIL.COM", "KARA28", "df7c916d04b08ed19ece0cb8a1750f3c913f8f76", "1-850-997-4695 x480", false, "0abf33ff43be5ea04f28a0d01f67ad8966e61b56", false, "Kara28" },
                    { "062fc4b2-61b5-4633-a3f0-487b72ec7f34", 0, "0caae795ae4f0d76407507f150a3e7ba8b8ad801", "Annamae_Gottlieb_OConnell@hotmail.com", false, false, null, null, "ANNAMAE_GOTTLIEB_OCONNELL@HOTMAIL.COM", "ANNAMAE_GOTTLIEB", "8a03604377d7466322075e8d9dab08e0cf86396d", "407-605-9715", false, "f7f02bcd169776492399683f9cb4e008c38de7ae", false, "Annamae_Gottlieb" },
                    { "09c7f01e-a1da-45d8-adfb-e476f8bc34be", 0, "65286f02bb0c444be8bf97696399b232c3773429", "Jerad6161@hotmail.com", false, false, null, null, "JERAD6161@HOTMAIL.COM", "JERAD61", "a9b0e45fd8bce67007c7ac59ff8f15e54d93d520", "1-401-444-0460", true, "1b045193d9ef511e311ec5d89581c32b5c6eb14e", false, "Jerad61" },
                    { "0a2f1d2c-16cf-4cc2-bfe8-19f21cf5ca76", 0, "e02545db45b0f4a0ae749ecbfe6a4f6c4be72ae0", "Adrian.Miller58@gmail.com", false, false, null, null, "ADRIAN.MILLER58@GMAIL.COM", "ADRIAN.MILLER", "543d4d440ade73d1786d2032075f4d3c7cac1b12", "723-507-2548", true, "591d6f95cf9eeb8511cc75f3616a900b7378e5c1", false, "Adrian.Miller" },
                    { "0cdc3c71-d27c-4237-9bf2-b3df7f321ed3", 0, "3603816c6e569b2e3a7a49782e5b2ad713b467e9", "Sibyl33_Schoen@yahoo.com", true, false, null, null, "SIBYL33_SCHOEN@YAHOO.COM", "SIBYL33", "19f5cd6de8c44a4760ef4a298d2f17d2a55db6ad", "(840) 634-6970", false, "4f125f9cfdfbcdf675dfa6b6aaaf56b061a48f49", false, "Sibyl33" },
                    { "0d7930af-dbf1-444f-ac09-524b43aef75f", 0, "6bd6234eea37d256965ec25f4587f6e957451e6c", "Rebekah7258@gmail.com", true, false, null, null, "REBEKAH7258@GMAIL.COM", "REBEKAH72", "2b0e380f4735baf7887f694f8771bb6d8c3a74e6", "970-253-4729", true, "5ef5391c449399e9706b0e4805df64f4a1a7aebe", false, "Rebekah72" },
                    { "11f23d4b-8c9e-4d05-8e06-5d0199b7f828", 0, "7f4765a178182d0e80e308627b190541fdec600a", "Ryder.Kreiger_Bailey@hotmail.com", true, false, null, null, "RYDER.KREIGER_BAILEY@HOTMAIL.COM", "RYDER.KREIGER", "04d8a9a3a423f8d0e05ce903d5efb34e49053d3b", "893.928.0622 x124", false, "a07e7fefa4cf32273aa5bb8704f587c0da2d22bd", false, "Ryder.Kreiger" },
                    { "1728fd06-3059-4676-bb37-52f287d075a3", 0, "7ba505c4213229de81cb3cdd8f3ab1b072efb1f6", "Ethan_Greenholt63@gmail.com", true, false, null, null, "ETHAN_GREENHOLT63@GMAIL.COM", "ETHAN_GREENHOLT", "a6aaa1fe41b779bafc01f0fd2568845ec5bafae8", "588.941.2595 x86616", false, "4e187ddbd240282a7d85b660bca1e0a4d7c1e655", false, "Ethan_Greenholt" },
                    { "1dfe8cfc-7485-4ff8-99d5-12a152a6fc55", 0, "8df7b701088e3d93062c7ad0e17e93fd468d9ba1", "Michale_Klein.Jenkins73@gmail.com", false, false, null, null, "MICHALE_KLEIN.JENKINS73@GMAIL.COM", "MICHALE_KLEIN", "5389c1639db45ffae247d9865a6d043f5d43f8e4", "1-812-571-3346 x4603", false, "c3cb9618d98be4dca141b9259ce7aca63fa9dcde", false, "Michale_Klein" },
                    { "2080611e-c96a-4962-9fff-600b46b62b19", 0, "6bad88f1f1045d6217be8c6590d0e670cad07267", "Andreane72.Roob@yahoo.com", true, false, null, null, "ANDREANE72.ROOB@YAHOO.COM", "ANDREANE72", "550f78a844994760cf4e0a8463c8854294cdff11", "(965) 284-0847 x710", true, "426b47c1eadc384bdb94b20e06ffa81ce39e3cfe", false, "Andreane72" },
                    { "217381c5-d8e0-4e1f-862e-cb07605f1153", 0, "22003d583dd3fbbb9145de02a6fca4302c7847b5", "Kelton.Ullrich74.Pacocha18@yahoo.com", false, false, null, null, "KELTON.ULLRICH74.PACOCHA18@YAHOO.COM", "KELTON.ULLRICH74", "5020780dfc2f3c14d26ce13b9eda4091e41aa646", "1-686-734-1201 x9598", true, "01076ecaa3dcc614c779b58e311ea796dfbc0c4a", false, "Kelton.Ullrich74" },
                    { "21bedbf0-7cc1-431e-8999-7df2f637c363", 0, "6d036c5873acab809c66c881d8214506914a32d6", "Rosalee_Stanton55_Spencer58@gmail.com", false, false, null, null, "ROSALEE_STANTON55_SPENCER58@GMAIL.COM", "ROSALEE_STANTON55", "0d120c95da570134afd4b087b545f984d93f5050", "1-275-910-9291 x1455", false, "4e1976f75c2be51f496a73542196e3fa9d0cfef5", false, "Rosalee_Stanton55" },
                    { "221e955e-8e7f-4a06-8b4d-acf7f89b0799", 0, "557af7a99ea91a03a533d785f8c05d3a4d9ecabf", "Ambrose_Barton33.Fahey34@yahoo.com", true, false, null, null, "AMBROSE_BARTON33.FAHEY34@YAHOO.COM", "AMBROSE_BARTON33", "a0701abd867efd5955b424f521678df2aa4bb094", "(769) 722-0454 x8151", false, "8f1981c1fdd8810601ef345b654489e5b7c236c0", false, "Ambrose_Barton33" },
                    { "22582e1e-da5b-48dd-b905-eb22abbf4d31", 0, "0e1815dd641b4a024ff7f903fa6db81ce7e25d53", "Charlie_Toy_MacGyver63@hotmail.com", false, false, null, null, "CHARLIE_TOY_MACGYVER63@HOTMAIL.COM", "CHARLIE_TOY", "1242616e943284fd791472f3560f82ab3c35d1a1", "1-375-430-2955 x8473", true, "9a580f51df04d55e31df165698fc1906e0adc635", false, "Charlie_Toy" },
                    { "287fc1ec-89ba-43e6-98c0-9bc1f61badbd", 0, "3778101070a80e5dd9a7d9b420840c31cc3e0184", "Weston.Kuphal2264@hotmail.com", false, false, null, null, "WESTON.KUPHAL2264@HOTMAIL.COM", "WESTON.KUPHAL22", "bacc79d3900eae2217bfaec080359fb5b6e8444b", "309.838.2673 x114", false, "673f10dcd1da4afddb46689a5a3309ed2093acf7", false, "Weston.Kuphal22" },
                    { "2d9fe072-3cce-4184-ba98-dc73023540f4", 0, "ed1f9f7672a15e7742cc2edb895637fd03616acf", "Madyson.Blanda4637@yahoo.com", true, false, null, null, "MADYSON.BLANDA4637@YAHOO.COM", "MADYSON.BLANDA46", "a9040e4499b54e7bf8a952cbbbecb39037a86e47", "(702) 416-3612", true, "fd8b74c0ac9a8cf8d595f896fb2d3e4042979046", false, "Madyson.Blanda46" },
                    { "2e32156d-aab6-4954-8194-29fabe25222f", 0, "2f29687c6b077ef7807807d34ac57b5d061fef39", "Lorenzo36.Lesch70@gmail.com", false, false, null, null, "LORENZO36.LESCH70@GMAIL.COM", "LORENZO36", "9c3a7dc06eadb5d33a461f4182db7bb21258992e", "922-311-5434", true, "91b90d0d8e43b4b98114f18a27c9049a75d04b1b", false, "Lorenzo36" },
                    { "2eb63f11-56f8-4d19-9f32-04da4fa24d26", 0, "1923aab133dfbeacb2756ef04922e0d841e5cdd2", "Alexanne76_Tremblay@hotmail.com", false, false, null, null, "ALEXANNE76_TREMBLAY@HOTMAIL.COM", "ALEXANNE76", "632055ada040b492848dd105265cc23d2470caea", "1-857-331-4233 x2113", true, "bca5d55ca97910143deffe08d7e6c95e6e216a8c", false, "Alexanne76" },
                    { "2f6e51b8-cc4b-4d85-906a-9467c9672c39", 0, "64f32153d524d9a294a2f2f9d9eca35a444aec7f", "Earl36_Turner@hotmail.com", false, false, null, null, "EARL36_TURNER@HOTMAIL.COM", "EARL36", "f6d5fe646e9763c432982cb114f84dd25a8d0149", "1-937-892-2330", false, "08eaa2ea81acc1d3ac4afef9072baf41df79d07f", false, "Earl36" },
                    { "2ff38e5a-4bad-4e95-984b-75fd4e64dd8c", 0, "4ad29a595828f1df04baddf7cae91691c7e30ae1", "Mabelle.Ratke_McCullough39@gmail.com", false, false, null, null, "MABELLE.RATKE_MCCULLOUGH39@GMAIL.COM", "MABELLE.RATKE", "6bc18399f2b5ac38b2019d19744bc662e72db95c", "(802) 832-7427", false, "edc73409e9dcd429a3a3eed8b96296c2d43b0962", false, "Mabelle.Ratke" },
                    { "3179bddc-fbd7-4c4d-aed2-e49bb31b12d7", 0, "59a08b68fa897aedf5ae37f48bc1f5a1d8206e47", "Karli75_Howell70@gmail.com", true, false, null, null, "KARLI75_HOWELL70@GMAIL.COM", "KARLI75", "6965ac727a6689ff329dcfabf2163a7763174d29", "599.409.3009 x319", false, "ee3dd2c8c22b1e36fb58a0ed3ff587a6ffeecddd", false, "Karli75" },
                    { "317dbf0d-2f90-45d7-8046-c47a9871dbf3", 0, "b0322cccfe0154a76b88d31e62914fe7eb2aa1f1", "Prince.Kautzer62.Rau93@hotmail.com", true, false, null, null, "PRINCE.KAUTZER62.RAU93@HOTMAIL.COM", "PRINCE.KAUTZER62", "da795cf686de2ecd63c85ec4b82127763add4325", "919-648-7763 x4872", true, "5ac6f8c1df5a96afd3edad41e7a80e82072b1642", false, "Prince.Kautzer62" },
                    { "324593c0-5ff8-4b07-abdb-31db1a2d08d9", 0, "6422d13d7987b3f4a50936efe4cbeac4ba9c379f", "Mae.Swaniawski.Hartmann@gmail.com", true, false, null, null, "MAE.SWANIAWSKI.HARTMANN@GMAIL.COM", "MAE.SWANIAWSKI", "eb490736310a0cac16ee0e3be06dcc0c0d9f0731", "(752) 729-9890", false, "a4b0ed91baff836f79bf159604933bba2d4b22d1", false, "Mae.Swaniawski" },
                    { "3806d912-5b3d-41ad-b581-a5f61f856071", 0, "40527e3c0e2215535b9ef0ddd269f3337592745a", "Andreanne58_Metz@yahoo.com", true, false, null, null, "ANDREANNE58_METZ@YAHOO.COM", "ANDREANNE58", "f930bc59e5206ed9face4a4e9e9821d08c487829", "1-771-603-1851 x35912", true, "0c8107a83f9edcb5557d3c7c8279a52683d812d2", false, "Andreanne58" },
                    { "397b72c1-5221-4502-bf17-1698b33c0f7b", 0, "af03d5d6491a7a3b2577226592efaf77f1e6ff23", "Larry_Aufderhar23.Dare24@gmail.com", false, false, null, null, "LARRY_AUFDERHAR23.DARE24@GMAIL.COM", "LARRY_AUFDERHAR23", "764eeeecb216f27c59da737eb59d9a9106d00a29", "263.412.7020", true, "c56079bac6d43924e6385020806947ca64e2c761", false, "Larry_Aufderhar23" },
                    { "3b0cef3d-fbe4-4080-8c9f-12b1d136d483", 0, "ce49a86e57c440f715b3979f89a3bec5c408a7bf", "Kyra_Frami_Carroll@gmail.com", false, false, null, null, "KYRA_FRAMI_CARROLL@GMAIL.COM", "KYRA_FRAMI", "f778bc3c77826d8beac587aacf53b1f18a0811d6", "(731) 535-7442", true, "62b3a336f267a958cccc6f0677abcfadc6e6c708", false, "Kyra_Frami" },
                    { "3c424d62-3eca-45fe-9e11-42f5430a2af7", 0, "860708331a89b0cd1d63d7f7de3240d2ecf4d510", "Ethyl.Konopelski3015@gmail.com", false, false, null, null, "ETHYL.KONOPELSKI3015@GMAIL.COM", "ETHYL.KONOPELSKI30", "8ce99408877cf811aa35bccc3091a6e21e58c187", "(550) 977-1066", true, "cfef88720365edcb0268bed184b039582c8cdd3a", false, "Ethyl.Konopelski30" },
                    { "3e920ffd-249d-4292-91a5-7661efdb5714", 0, "cefc67c4b536a0e5d3a040789b032e052053e897", "Rachael_Hagenes88_Moen@hotmail.com", true, false, null, null, "RACHAEL_HAGENES88_MOEN@HOTMAIL.COM", "RACHAEL_HAGENES88", "1f5a032501a438591d1daa9e1e13103102c9d33f", "1-944-432-6127 x2322", false, "4129981344ab5b7767921e4fe830bbbea1987048", false, "Rachael_Hagenes88" },
                    { "3ed20b3b-b45e-4f49-abbd-c4f1ee47abe8", 0, "3d21d07491dbc771b3230950dc74bdfad004ecbf", "Timothy6112@gmail.com", false, false, null, null, "TIMOTHY6112@GMAIL.COM", "TIMOTHY61", "e745b643ce83aa46f134350eb74243d4209415ba", "891.268.9008 x3625", false, "2ac36246025ddd68769321e9031a0f5959289d89", false, "Timothy61" },
                    { "49d6c4ac-a88b-4a47-a100-b13808162f23", 0, "34bb99a6abc91bad676e451c94683395752f414d", "Horace.Becker.Dickens72@yahoo.com", false, false, null, null, "HORACE.BECKER.DICKENS72@YAHOO.COM", "HORACE.BECKER", "0f155d33729542ba2c6a4dc20fa379ac7ee0752d", "559.757.9806", true, "d34db1b68c0110423e463fcd7dd64b2ca8d19e7e", false, "Horace.Becker" },
                    { "4f59fd58-b7f4-45b0-a673-b2bf2a17f898", 0, "d22dfb0552acf58cd432f49834a1b11ff9b96a3d", "Nigel_Zboncak65@yahoo.com", false, false, null, null, "NIGEL_ZBONCAK65@YAHOO.COM", "NIGEL_ZBONCAK", "f48fb3caba5c8a78ea3a620f6173391058fe58e4", "268-733-3442", true, "556b66341696003d3899c872847fb835be41b480", false, "Nigel_Zboncak" },
                    { "50244665-ca1d-4058-8da0-d1e8f79b1595", 0, "3ae2ec44740ad27bd8d3659da90b8fdb482745d4", "Daisha.Morissette9895@yahoo.com", true, false, null, null, "DAISHA.MORISSETTE9895@YAHOO.COM", "DAISHA.MORISSETTE98", "1a6029a140971ef9ccf2cb642c7798061ea4cc99", "1-205-711-9956 x96609", true, "e53d6b8d86a31c6532e6138b03534ac0264778d6", false, "Daisha.Morissette98" },
                    { "54d6e8ea-8ccb-4490-b126-7264f2b2e87d", 0, "d24208b666b456de943f35d5aa81fdfbcb32b4b5", "Owen.Bruen68_Flatley2@yahoo.com", true, false, null, null, "OWEN.BRUEN68_FLATLEY2@YAHOO.COM", "OWEN.BRUEN68", "40260a482ce6a4a9d567bab1be4416efeff04173", "299-403-4361", true, "e001eb020f66488950b15f6bde7b5a4be38ba93f", false, "Owen.Bruen68" },
                    { "5d0e6e1a-290d-4565-a7f4-f281776d217a", 0, "1ef65f2e28c8f234dcb8b27d710d0dd2848191b4", "Elissa_Hilpert_Brekke96@yahoo.com", false, false, null, null, "ELISSA_HILPERT_BREKKE96@YAHOO.COM", "ELISSA_HILPERT", "4c5fc9327b844e24047eb244d29b9674f5a480c3", "(571) 810-3075", false, "d5dfc01bb21811317019f881445f4ebf7315448d", false, "Elissa_Hilpert" },
                    { "5ff97174-0e00-4df2-8ae7-6b8646105723", 0, "0c9901c3c7b71e3773619766d86e0fe65b09bc73", "Mackenzie9687@gmail.com", false, false, null, null, "MACKENZIE9687@GMAIL.COM", "MACKENZIE96", "c0395038c1555ef7038dbdad44ad13c94861d6e9", "901.215.3654 x85995", true, "cdcbcb4d472bc0cf564055684b1c7eaa4e0d0f09", false, "Mackenzie96" },
                    { "63d951a3-4642-4000-9953-8aca76e4cde8", 0, "195c23e8312d25d0e0e75c7e767c33836ca6ce8d", "Hailee.Ziemann4530@yahoo.com", true, false, null, null, "HAILEE.ZIEMANN4530@YAHOO.COM", "HAILEE.ZIEMANN45", "f1c5af07fbc626866111296dcffed4beb17dcb1d", "1-385-875-7397 x86032", true, "18be40d6902803d6bf8bf1aa0da0ea9b03fad78b", false, "Hailee.Ziemann45" },
                    { "654121b8-d8f1-4b42-ae96-239cba90f193", 0, "282cff00924d13c4ce0fc455a5c69f8ab1572168", "Ervin.Greenfelder_Oberbrunner@yahoo.com", true, false, null, null, "ERVIN.GREENFELDER_OBERBRUNNER@YAHOO.COM", "ERVIN.GREENFELDER", "3d8b26ad06e8c624dce798d93a65fad878e4c8bc", "235.762.8361", true, "9a485e8d32ad87ebff7705e686148d7502de3535", false, "Ervin.Greenfelder" },
                    { "662898a8-63c9-41d4-b031-669d5c3c7f85", 0, "705b1a809cbcdae5efe4f60e6702b6f7f86db016", "Giuseppe6677@yahoo.com", true, false, null, null, "GIUSEPPE6677@YAHOO.COM", "GIUSEPPE66", "d39ba65a345b103760e3c0dc9d75e03c92a1ae8d", "253-406-8152", false, "5a2fe056f3f8398d5d1de0b2191e61c781d2eee8", false, "Giuseppe66" },
                    { "671d3739-ea4f-4f3c-9668-4edd6c66f7d7", 0, "9d048ff174f71cc30c5ac473e40779b9ad78670b", "Rossie_Mueller58@hotmail.com", false, false, null, null, "ROSSIE_MUELLER58@HOTMAIL.COM", "ROSSIE_MUELLER", "f7be7d71124d2057c60e64eee5605884cbf851e3", "(988) 966-6005", true, "9c50acb800084b3a8df166c8a0061c1b8d49fe32", false, "Rossie_Mueller" },
                    { "686830cb-c9b5-44ee-aa53-09e48723fd1a", 0, "998759385d7e7597ea03aa595fa58aa8e8ebd773", "Kade10_Dare@yahoo.com", true, false, null, null, "KADE10_DARE@YAHOO.COM", "KADE10", "2db632e837395e3f8390af987bd896934d0c17df", "(809) 395-9069 x87079", true, "ec7d4f23020f0b7243f756ecd058e7d15a2d8b47", false, "Kade10" },
                    { "692700d6-d4cc-4f6e-8a2d-8e2bd692b1c4", 0, "223c6e8ba05bcfa6b5a854e6e75fad591ffc595e", "Fredy.Abernathy52.Goyette19@yahoo.com", false, false, null, null, "FREDY.ABERNATHY52.GOYETTE19@YAHOO.COM", "FREDY.ABERNATHY52", "0ed5b383e46e86c5940bfee5c84fd17e26253619", "1-210-544-2731 x37829", true, "f027f161c1a867b10792f85cd7b1362326cec65a", false, "Fredy.Abernathy52" },
                    { "695f92c0-0873-43cb-9cc5-f322bb297a93", 0, "bc81916d039ae977d5bcbf01025451d40eca464a", "Chris45_Bernhard@gmail.com", true, false, null, null, "CHRIS45_BERNHARD@GMAIL.COM", "CHRIS45", "ff5035e1983c1a8f069e2a97ece19ac6de80700e", "399-380-7119 x040", false, "4537aa12971af0e46d276c8ecdf2287f5bac72d3", false, "Chris45" },
                    { "6b77b213-ce1d-4ead-8026-4974abbc926b", 0, "3e56e6ac6b45d7d919ed8583ff8a5e289aa61878", "Isabella.Mayert19_Kunde80@hotmail.com", false, false, null, null, "ISABELLA.MAYERT19_KUNDE80@HOTMAIL.COM", "ISABELLA.MAYERT19", "263071cdb4f3a3270c335f6859c5d5556ad5f852", "265-616-9162", true, "00fd755e077ea9320aae46f5ead05d776035081f", false, "Isabella.Mayert19" },
                    { "6d1cd21b-2ed4-4fa0-9a39-1dc9576c69f2", 0, "56261d077e508217f54b95233136bd288e5dee55", "Ashton_Erdman11.Mosciski86@hotmail.com", true, false, null, null, "ASHTON_ERDMAN11.MOSCISKI86@HOTMAIL.COM", "ASHTON_ERDMAN11", "f7c20ccc9b48f88aa42d35b0f58b71837491dbd5", "(700) 373-2780", true, "83d7caf191fa20dacc3a3047311af6de6c507334", false, "Ashton_Erdman11" },
                    { "739375cd-ec7d-40ad-b7be-8dc294e2167e", 0, "b73aeec280e3ef7c94efcc1c60a7fdf62d5cf0d6", "Harrison.Hagenes32.Hickle@yahoo.com", false, false, null, null, "HARRISON.HAGENES32.HICKLE@YAHOO.COM", "HARRISON.HAGENES32", "99f8f8404cca015acd59753c3f51576fb94463fb", "871-906-9286", true, "f404cb4eda982904625033bc5df7c149daa34ff7", false, "Harrison.Hagenes32" },
                    { "75ac5619-837f-40db-a47a-e36b5f74ddcc", 0, "de6296ba8383535d389b86c8ad567ee1ba0fa516", "Isac_King9233@gmail.com", false, false, null, null, "ISAC_KING9233@GMAIL.COM", "ISAC_KING92", "1c5504ca3814058f40755a8f636b28a7909c9313", "1-252-517-7964 x95974", true, "eb3653684fe4bf8645edd08bdaafbc51f47972a3", false, "Isac_King92" },
                    { "75acaca5-a4fa-4b64-b78d-4eb4c25f2cd0", 0, "ff0a5fee2fc68da032be81204a1b7cf26d88a5e3", "Derrick_Toy11@yahoo.com", true, false, null, null, "DERRICK_TOY11@YAHOO.COM", "DERRICK_TOY", "4286ed7f0ab9d8a83c52def7665cfc7cd8507762", "828-362-5443", true, "b37ed465b567ee5630f87bf43ec53a1b627960a2", false, "Derrick_Toy" },
                    { "75e8d2de-693a-4cac-a185-81d91a723ac4", 0, "1e0aee6c6c820ea1c1879ed0aab82a85a8e9f2af", "Alexane.Aufderhar60@yahoo.com", true, false, null, null, "ALEXANE.AUFDERHAR60@YAHOO.COM", "ALEXANE.AUFDERHAR", "5796d66ae45d401eea6c04435de7c663a6db71e2", "(538) 538-8665 x5319", true, "0aa69dcf6178dd0135007fd8d13d267dc4b59da8", false, "Alexane.Aufderhar" },
                    { "77b5dd70-4165-4149-8a17-d0d0c0206fa9", 0, "103e933b70a69a631125639018c1ad197955fb30", "Delilah_Lowe15_Goyette11@hotmail.com", true, false, null, null, "DELILAH_LOWE15_GOYETTE11@HOTMAIL.COM", "DELILAH_LOWE15", "a5371cf98cd8148f383e14197b6ceb7cea7e090b", "879-949-8022 x14708", true, "6cf76096901392364a0652ad4096d9e977854a26", false, "Delilah_Lowe15" },
                    { "7dc1e8d6-d2ab-43d9-b789-60e36ba2c868", 0, "c4d0c94d536b7c567bdd1b44f66f35df20eefdc9", "Shirley.Kilback.Treutel@yahoo.com", true, false, null, null, "SHIRLEY.KILBACK.TREUTEL@YAHOO.COM", "SHIRLEY.KILBACK", "b02d0399caef5b854f711984f50cc282e74e9579", "1-321-504-7543 x568", false, "3550929bec5ee1217dda3dd92c0d79cf8edc48bc", false, "Shirley.Kilback" },
                    { "7e4d7f90-a5dd-4dc8-9c50-271678a09e7c", 0, "8bdafae8688524f3076c55fc8d0af8f6a39c5931", "Georgianna9618@hotmail.com", true, false, null, null, "GEORGIANNA9618@HOTMAIL.COM", "GEORGIANNA96", "7697df0cc7a61a3156fc81120c7e89b1f3dec957", "927-505-8512 x9598", true, "64b25c4b1d9136d557718233c87ec55b18646ec3", false, "Georgianna96" },
                    { "7fd1ecfa-6813-4b0e-bc10-b5ff0808d761", 0, "86a79be46e9fd7687661916d060c9554d1674e56", "Rollin_Kub.Keebler@gmail.com", false, false, null, null, "ROLLIN_KUB.KEEBLER@GMAIL.COM", "ROLLIN_KUB", "374eeb353d0f60e896f231d26fd3e4b481031564", "301-944-5209 x38284", false, "8d58d06697a063b1917fef6f2b640b640aa5c446", false, "Rollin_Kub" },
                    { "8f9ff296-4892-4c2b-bddf-9b3d6ef3a49f", 0, "23a979c031f152fd9f5754cc7fcf9da7e06e6eaf", "Gus_Kreiger.Goldner78@gmail.com", true, false, null, null, "GUS_KREIGER.GOLDNER78@GMAIL.COM", "GUS_KREIGER", "3b78b598aa3a69bf47b8bd069238f8154a7a13a1", "(400) 677-6689", false, "e21d5b0fd5089375a0e821828d6c21e0c9a9b31e", false, "Gus_Kreiger" },
                    { "9021f782-ecaa-47b1-ae30-eb058d7aec60", 0, "f6c8c8e99f61e9bde3e45998f00008ecc2b6c45b", "Shayne13.Christiansen24@gmail.com", true, false, null, null, "SHAYNE13.CHRISTIANSEN24@GMAIL.COM", "SHAYNE13", "3bd3a17fc838841b3065b48db05d2a4d81e91637", "531-859-1200 x749", true, "6b52a50f09ecfe8e3f8ac9c1350667adceeff636", false, "Shayne13" },
                    { "91b98aa0-67d8-46c8-9485-8e8b27afc034", 0, "a3407a28a2d8273d84fc5ead8256ee673f04eb05", "Sherwood.Volkman89@hotmail.com", true, false, null, null, "SHERWOOD.VOLKMAN89@HOTMAIL.COM", "SHERWOOD.VOLKMAN", "5ddced79bbc44ba92d2045217364e4c16927471a", "432.284.3214 x327", false, "03bf8293fdd1eeefab4a73b7fbda825ac8f09384", false, "Sherwood.Volkman" },
                    { "9879cacc-9f83-46c3-97c1-3c51f87ec41b", 0, "5c074a65fa68d8a301829ec972b3353554e9ff81", "Wilford.Welch55.Mertz89@hotmail.com", false, false, null, null, "WILFORD.WELCH55.MERTZ89@HOTMAIL.COM", "WILFORD.WELCH55", "42aa6bdb22442dbf7e1e7f07026d367c44f8b7d0", "(728) 971-5459", false, "37d4bacadc2c5f4512a45f7cb80ed34f11bf1cc6", false, "Wilford.Welch55" },
                    { "9ca8fac9-d2a3-4cb8-8341-b0d9f03cd16e", 0, "8760a4beec92b0b07e29275842b6996e1ab038f1", "Melody_Langworth_Hayes75@gmail.com", false, false, null, null, "MELODY_LANGWORTH_HAYES75@GMAIL.COM", "MELODY_LANGWORTH", "6537fed4a30e58ba02b614704b63bfda49e74d43", "786-824-8776 x7743", true, "a098fc3fca59e5a2d2237a9a55aba352d7c1bf08", false, "Melody_Langworth" },
                    { "a15421c7-a19f-423d-ba0c-8d11de926a99", 0, "89baad2f36931f4de2e77ebf1431d41e3edfcc89", "Loraine1129@gmail.com", false, false, null, null, "LORAINE1129@GMAIL.COM", "LORAINE11", "29baa8ba7d6ae7998a86fb0c84f2246fc1e2335d", "(918) 621-3416", true, "6655f5c57799cdd79f25987377dd97645b095bc3", false, "Loraine11" },
                    { "a3aeba4d-99c2-4531-9c34-1562f499c716", 0, "e4357f64cbd9271bc14ad8521da73a1f1e1657e8", "Glennie_Grant344@hotmail.com", false, false, null, null, "GLENNIE_GRANT344@HOTMAIL.COM", "GLENNIE_GRANT34", "6970893b348eb10e7a40c5b957faddc64abb7e7c", "464.473.7403 x8509", true, "496dd79b776d00d0ddb0a8d58291556928c2fecd", false, "Glennie_Grant34" },
                    { "a5a21107-a70d-45d6-9442-13ac8bf026b1", 0, "4574da928944e5e6d27f37a44778c94ce234dea8", "Michele.Schumm2784@gmail.com", true, false, null, null, "MICHELE.SCHUMM2784@GMAIL.COM", "MICHELE.SCHUMM27", "36b77006da9c1e04c30ec4a59f5b457a1e809bd8", "(687) 741-9579", false, "b09adeb04f8d0e6ec5d8a4272eb360a0572caf7b", false, "Michele.Schumm27" },
                    { "a606b64b-d2a7-4f04-9fc2-117e5790e827", 0, "2929dd2ccf14dc30f638c320c4e37092839183de", "Jayden13.Cassin@gmail.com", true, false, null, null, "JAYDEN13.CASSIN@GMAIL.COM", "JAYDEN13", "2448f400768462e816a28d9dd3953c91a84cb78e", "(980) 258-0537", true, "bc10f71029ebfeb5b6ababdbc6add2cadb6bb8e6", false, "Jayden13" },
                    { "a98dc326-ceee-4204-8f0e-da3cb6d2031c", 0, "518144ddde3f0f598426f3eaa9603b70e8e9cd03", "Rick62.Wisozk@hotmail.com", false, false, null, null, "RICK62.WISOZK@HOTMAIL.COM", "RICK62", "cd48b5d4151bb7366636cf3607a84cd09ccce830", "471.258.2886", true, "e899612902793bfd9e73a4f5702ea68e227359fe", false, "Rick62" },
                    { "a9ed8cec-2aa2-46e3-89e5-5fab93901028", 0, "3e5a04812310e1753a580bd698115859ad00342f", "Shawna.Gaylord.Bode@yahoo.com", true, false, null, null, "SHAWNA.GAYLORD.BODE@YAHOO.COM", "SHAWNA.GAYLORD", "0e23212d8fb33e402ae7dc7dce38d5b1f4786556", "(227) 858-2013", true, "f3da23a523469d246564e9f3493e7c9c588aedb9", false, "Shawna.Gaylord" },
                    { "aa2318fa-5b3f-4d92-a38b-dbddee742a74", 0, "1ed5c44d8b4b0a88a0762539a92666e3e2d1fdca", "Sarai_Pacocha52@yahoo.com", false, false, null, null, "SARAI_PACOCHA52@YAHOO.COM", "SARAI_PACOCHA", "d657835b06a555fbbaa424cdd937878c124f3999", "(537) 770-4535 x5912", false, "6ea6568592b374a8a79e0d39ee33108111d4a687", false, "Sarai_Pacocha" },
                    { "b46829f5-d7e3-4cce-8441-54415c687d76", 0, "ad8ea4c7138cdeb6bb90da3a3ced6391272bf35c", "Joel.Kilback37.MacGyver19@hotmail.com", true, false, null, null, "JOEL.KILBACK37.MACGYVER19@HOTMAIL.COM", "JOEL.KILBACK37", "67e1e40eb597315e7c45f305d1b8cb4a466051b4", "1-747-785-1303", false, "eebae07e0d1fed42794c882f0e2db5950071866d", false, "Joel.Kilback37" },
                    { "b5515c5c-9a6b-4b68-8c33-c66fd05ffced", 0, "f2d40592454fe3bc881600b62664e0a3c1372ec6", "Ona13_Erdman@yahoo.com", false, false, null, null, "ONA13_ERDMAN@YAHOO.COM", "ONA13", "2bfa2e73426dbf8421c51ea57f471765aba59264", "(299) 771-4889 x064", true, "ca4d74019d71c8740a32902cb13053b0b7969cd2", false, "Ona13" },
                    { "b5d57004-3dde-4ab0-9b5b-0d701b10eceb", 0, "0003540446f240193d28630d975928a2dc5bbebd", "Kyle.McClure.Russel@yahoo.com", false, false, null, null, "KYLE.MCCLURE.RUSSEL@YAHOO.COM", "KYLE.MCCLURE", "8ae86d453c9c98e5a64b5660901722a1cd84046d", "(756) 610-9377 x694", false, "55e406f07fc518824ef5cc2027fc0a55075f8102", false, "Kyle.McClure" },
                    { "b7a3ba9c-206a-45ce-9e12-8bd716b19d81", 0, "cb5d2816c15bb0a20948f5bf666c5ce2233b8753", "Judy_Russel.Cummings@hotmail.com", false, false, null, null, "JUDY_RUSSEL.CUMMINGS@HOTMAIL.COM", "JUDY_RUSSEL", "a6dce2bc91567b1fd66e962a3dcd62439b59e400", "522.948.3671 x78444", false, "06109c1188fc13e443906d57c1db581955d25106", false, "Judy_Russel" },
                    { "bc42a373-93dd-469e-a8d7-d1bf79e905cd", 0, "0537c3c82b4bb634b4a9801bb1be5aac0496688e", "Josh.Gerhold_Schmidt@gmail.com", true, false, null, null, "JOSH.GERHOLD_SCHMIDT@GMAIL.COM", "JOSH.GERHOLD", "23e18a0de1b09b6b5041a8a92ed6d9ff1b5d0aff", "562.790.6092 x71619", true, "eb8c58e836dc399a95f9c9f41bfd68bb143f0d4a", false, "Josh.Gerhold" },
                    { "c2e6bc61-83db-4806-9518-1f319daabe61", 0, "fbf0ca79bdadf4f8dc152e776fffb050ccee3a49", "Sarai42_Hayes@hotmail.com", false, false, null, null, "SARAI42_HAYES@HOTMAIL.COM", "SARAI42", "b8ef1b739e55a60fe47721ebc30378b50584f229", "1-700-718-8927 x459", true, "c8d4573537c1d6aaa22da3b13045eb0c1eb94955", false, "Sarai42" },
                    { "c4ec171f-9949-491d-9262-4138e1c4a353", 0, "ec67cfdaedfecc68b98fb61a4ca473926a93a51a", "Alex.Skiles77_Lebsack@gmail.com", false, false, null, null, "ALEX.SKILES77_LEBSACK@GMAIL.COM", "ALEX.SKILES77", "3539bf33715b893e41d7182c13f2596819715346", "921.262.9037 x44005", false, "9973c552cdd7b6ba267700deadaf1280c6e70f0c", false, "Alex.Skiles77" },
                    { "c5c8b809-cab5-4210-aa91-d078df2dd2dc", 0, "158588469e0a317a4e2b160b02a51ff146b8cdab", "Muriel.Reinger45@yahoo.com", false, false, null, null, "MURIEL.REINGER45@YAHOO.COM", "MURIEL.REINGER", "6590dd270159c12218132bdf50fd4dd4a4894ff6", "565.928.4254 x8352", false, "0705bff94e5404ba89bde53230e899e29b2bb745", false, "Muriel.Reinger" },
                    { "c938a2ac-4e2a-4eee-b6d7-bea8269ce91d", 0, "8f0b47aaf9d3b758965e66aeef3663b999f6c13c", "Dillan_Emard10@yahoo.com", true, false, null, null, "DILLAN_EMARD10@YAHOO.COM", "DILLAN_EMARD", "f98ea838f327a02724c091b6b226c8f1b67a4efb", "296.495.8616", false, "55d8f1a36138617ed1385cc730b3872450a33379", false, "Dillan_Emard" },
                    { "c9aed305-8c2f-4ee5-bb74-8285ccb7ba63", 0, "ee11c48324bdc589db22332447e656943a8dd9ae", "Howard_Harber10_Ankunding@gmail.com", true, false, null, null, "HOWARD_HARBER10_ANKUNDING@GMAIL.COM", "HOWARD_HARBER10", "5b64f8083fa6beb647b9465558c1ddd59420fbc4", "640.457.3849 x5870", false, "882570adfb09c52e51e7cfb624c6bb79a60c40e4", false, "Howard_Harber10" },
                    { "cb07f1ba-abef-4a56-af75-326127d0c7e4", 0, "d4809db0e9d89f771157e532f59375cd05bdef23", "Ari22_Swift@gmail.com", true, false, null, null, "ARI22_SWIFT@GMAIL.COM", "ARI22", "277f11a68d27735dde71f079a7026dcda2bd5e88", "896-593-4478 x2830", false, "e1c5cbaba64f2a41fd4917fb04d4887f5f3e42df", false, "Ari22" },
                    { "cc535053-1f78-48f7-910b-1bbd66cebf0f", 0, "fc7f875365c7c286a4b29be0b328891bdccb946d", "Fermin60_Kutch5@hotmail.com", true, false, null, null, "FERMIN60_KUTCH5@HOTMAIL.COM", "FERMIN60", "94ce50ad3dec3761cc0725d0598a938ec58324e0", "(853) 547-7466 x8061", true, "6e40299c3ab189e37b8b703f40fde2e7e0041cdc", false, "Fermin60" },
                    { "ccea2f5e-fe5f-4ae4-a519-06e015f13a96", 0, "cdba32418682c1bae4e8994af7f1ea5485a7b050", "Mireille_Streich37@yahoo.com", true, false, null, null, "MIREILLE_STREICH37@YAHOO.COM", "MIREILLE_STREICH", "d953bf977f632036f013abb335a2cdb532864c70", "812.593.4250 x4601", true, "0ae2989c719812a2d20656b930ff8c22803fc99a", false, "Mireille_Streich" },
                    { "ced85e1c-8776-4da0-8a20-af7d14e6e373", 0, "f30e27215d9a8cba6c2c0f2c229836767866f911", "Kristopher92.Borer95@yahoo.com", true, false, null, null, "KRISTOPHER92.BORER95@YAHOO.COM", "KRISTOPHER92", "e4b9fba2bb16645ab40bd3559819a7265c880835", "(282) 396-2935 x331", true, "ae0c629e9c5a336af6f690739a19bca31b0f28e7", false, "Kristopher92" },
                    { "d06e6e86-84c6-4f47-bd0d-2bca667e9452", 0, "7d5bfc5c2b8e0031d3ddf641764fef97e78465f4", "Jettie_Leffler.Douglas14@hotmail.com", true, false, null, null, "JETTIE_LEFFLER.DOUGLAS14@HOTMAIL.COM", "JETTIE_LEFFLER", "6e576c2a9f7faacd8e580fab45a69ae4632c7305", "(884) 442-8862 x59870", false, "de2e08dd0da1fa0639302c1f13878f54d0b1dbbb", false, "Jettie_Leffler" },
                    { "d0c552a0-74e7-47d4-a62e-b2a6c5c3ab59", 0, "dbcc9dd0d5e10978088b1196ee2da3f873250512", "Selina.Leffler3039@gmail.com", false, false, null, null, "SELINA.LEFFLER3039@GMAIL.COM", "SELINA.LEFFLER30", "a353d62c8e458197d8f9cf9dcd549de3d9189798", "561-273-3065", true, "6328d80a8d405dcb977f0d2af17e01e6e738c103", false, "Selina.Leffler30" },
                    { "d145aaec-023c-4794-98ed-320e09c1c9bd", 0, "fd90902a65ab9c50a14be8c1c3c42ac9b87027dc", "Jett.Connelly55@gmail.com", true, false, null, null, "JETT.CONNELLY55@GMAIL.COM", "JETT.CONNELLY", "6c9d761071f757ef9a09d122018480439ca53633", "459.305.6470 x479", true, "d2817c2aa8641e55a7c6dc87dbe9d9c37cca80b3", false, "Jett.Connelly" },
                    { "d8afe971-bbf5-4065-a07b-c16202dd68b1", 0, "35f1b94ffd1b1e0bb7a074d4351397cbd6a6a434", "Marco.Hackett1484@hotmail.com", true, false, null, null, "MARCO.HACKETT1484@HOTMAIL.COM", "MARCO.HACKETT14", "ded78d4dbcadde8bd0d45cd07a297cf88ae281cb", "512-346-3025 x86919", true, "51671d7ca60649b10c0dba5fd152f53b41f455fa", false, "Marco.Hackett14" },
                    { "d8de05ef-06ef-4d4a-adf5-9f2195eff38c", 0, "1c5ace1feffdf6528e67f7dae5cf6f0909a810bc", "Elissa_Rohan_Yundt@hotmail.com", false, false, null, null, "ELISSA_ROHAN_YUNDT@HOTMAIL.COM", "ELISSA_ROHAN", "276fafa525e48eaed38683f6bda3abaf34d6d43b", "(203) 425-5468 x94476", false, "93517fcde0fcd45e7f4c56a7bf040419fdd5eeeb", false, "Elissa_Rohan" },
                    { "d9a6dafa-9785-45d1-9db1-f87b17eb1633", 0, "b657b9e2dcbdd383ffe8acb8697ae908318eb346", "Casandra_Franecki34.Fritsch@gmail.com", false, false, null, null, "CASANDRA_FRANECKI34.FRITSCH@GMAIL.COM", "CASANDRA_FRANECKI34", "fcd3c1a203b187b5ca5ddf9e17c97131a8cebf8d", "(480) 279-9931 x0813", false, "4bfe21b6405927b03e1440bf096fac9c2892fbb0", false, "Casandra_Franecki34" },
                    { "dcffcf50-b49e-4ca6-af79-b310095dda36", 0, "1a4898195521543eeec9492a1b1dcda0ab728f50", "Vance_Gulgowski2099@hotmail.com", false, false, null, null, "VANCE_GULGOWSKI2099@HOTMAIL.COM", "VANCE_GULGOWSKI20", "2920d4c6efbe0f4d5997927ab56fef6725549bc1", "1-328-412-5392", true, "a6d5c04ca55e68ff1858dc8f83ca903ac8c251da", false, "Vance_Gulgowski20" },
                    { "df0a708a-f3b1-4651-9b11-5f5f78ec370a", 0, "3a6eed1616833c3b85bf7c3f01ed2e0a177ea491", "Lloyd26.Parker60@yahoo.com", false, false, null, null, "LLOYD26.PARKER60@YAHOO.COM", "LLOYD26", "07b05af1379cccc9d38abeb33b2edc8b6075b07b", "999.765.9514 x6349", true, "cbd9dd887fdbafe1c8ca8137ed6c57ecccdf0165", false, "Lloyd26" },
                    { "dfd1d64c-76f9-407d-b4ee-3d8d3fc76ca0", 0, "aac1a206e98491a085423e29f81ece55049bc75c", "Nelda.Deckow5577@gmail.com", false, false, null, null, "NELDA.DECKOW5577@GMAIL.COM", "NELDA.DECKOW55", "132d67dfb5c7a7b3172c5908e2256b50612be85f", "1-774-709-0517 x8544", false, "8153e487951300283af3589ffb195f5912343d9a", false, "Nelda.Deckow55" },
                    { "e083048b-ef73-4afa-bf47-a1d436368488", 0, "8ab0b43ab19ad2bca652069d4b71224d77f21b32", "Natalie.Stokes36_Torp40@gmail.com", false, false, null, null, "NATALIE.STOKES36_TORP40@GMAIL.COM", "NATALIE.STOKES36", "2cac3145afb3aa88e70679d2fb48ab8caea93ab8", "(993) 264-5803 x8792", true, "965b0a2ad9f49f38f8d8ccbf6e9c0f54c8d03115", false, "Natalie.Stokes36" },
                    { "e6ecddcd-c542-448d-87ac-035a6bc6a924", 0, "b5e13b30218f5b6e44f9f42ce411482324ce1242", "Alice_Hansen83_Gusikowski34@yahoo.com", true, false, null, null, "ALICE_HANSEN83_GUSIKOWSKI34@YAHOO.COM", "ALICE_HANSEN83", "32a754d8c051bee648b7cee93d0c4414ed6d093b", "(345) 733-5568 x81204", true, "e9cda32ae3061db440474048e03d815b408320ef", false, "Alice_Hansen83" },
                    { "e7335c4f-d11b-499e-aed1-80dd190761c1", 0, "76e68f3f21f7f07c5a0f6066eded395d4ba0d4cc", "Jess.Harvey_Schinner95@yahoo.com", false, false, null, null, "JESS.HARVEY_SCHINNER95@YAHOO.COM", "JESS.HARVEY", "bc2c06ff372adc3b50ea825ffff24ec035a6bc2e", "1-508-446-8421 x55974", false, "9ffc1d883e6cc42863d17ca4b1772df9df1839e2", false, "Jess.Harvey" },
                    { "e7bb7648-6876-4e93-930c-d3a43bec1a25", 0, "6fe422d8b9df1399a799614eb22c3d1256fe574a", "Maida_Frami77_Heller71@hotmail.com", false, false, null, null, "MAIDA_FRAMI77_HELLER71@HOTMAIL.COM", "MAIDA_FRAMI77", "69fbb1c01a2bc52bc12ac9e865384bd49f676596", "(638) 233-2116", true, "d6e678f3909a703ca746d3c6de1dfa568bc5b71d", false, "Maida_Frami77" },
                    { "ea561aa1-2e26-4fed-88d1-c2a272dff04c", 0, "cfe23403f935de11252fc332c7a43903a1c400af", "Juanita_Marks29@gmail.com", true, false, null, null, "JUANITA_MARKS29@GMAIL.COM", "JUANITA_MARKS", "137172b8a9528bda9d187a387ddb5c50fb034675", "(904) 803-5649", true, "05364f0dc3900bb70e994f3e453daa626b817c38", false, "Juanita_Marks" },
                    { "eb9eb27a-74c7-4b45-88c7-fdf70f03144f", 0, "91d58467aa283057493c386118172c167f6f6117", "Trever988@hotmail.com", true, false, null, null, "TREVER988@HOTMAIL.COM", "TREVER98", "4095f478f39296b4d1fd02d70f340578ccb5a805", "(901) 310-0269 x3581", true, "bc20ea526bd2141131500379c16db5a40914fc95", false, "Trever98" },
                    { "ec682200-0c17-4ee8-ba77-d846fe1aebee", 0, "72cd9d4d5d8c46861bc6beb44080d11598f0ac14", "Brenna.Stark5633@gmail.com", false, false, null, null, "BRENNA.STARK5633@GMAIL.COM", "BRENNA.STARK56", "015bdee10797e1ceadfa2a49e5c784598e3570eb", "208.763.5743 x5675", false, "da6e7aeabaa84a85cbc7279b10e0e669f4d74623", false, "Brenna.Stark56" },
                    { "ec8cb310-2b96-4130-ba2a-3ae376bc6452", 0, "98d440f782c86ce3ec8631f41db1d2e08d464896", "Zella_Waters_Cole@hotmail.com", false, false, null, null, "ZELLA_WATERS_COLE@HOTMAIL.COM", "ZELLA_WATERS", "a9d5b94c00a2bc2e181983409da3f295a89251ad", "202.706.1319", false, "4aafbb425be962075dbef1cd64822090d45d844e", false, "Zella_Waters" },
                    { "ef0cd912-447d-4e11-87e9-ab34122eebbe", 0, "07b2d23efe30d7e91e5c5987b9b269e21a4c7f1c", "Rosalind1036@gmail.com", true, false, null, null, "ROSALIND1036@GMAIL.COM", "ROSALIND10", "bfbe2716ccd0f0df1c8d6d131e3e4ddd7bc18ac1", "735.445.5450 x0368", true, "c3d4dedc320b98cde8d4321ef0843b760612fc62", false, "Rosalind10" },
                    { "f5e6ba29-d5ae-4296-8a5a-ac7164afd84e", 0, "07e5df075738a33ac3d1a7828476c32e5b94cb33", "Cullen_Rodriguez_Emmerich30@hotmail.com", true, false, null, null, "CULLEN_RODRIGUEZ_EMMERICH30@HOTMAIL.COM", "CULLEN_RODRIGUEZ", "6dabac32b763a003681e6b8281f80a37d1180dc0", "1-942-468-5897 x35085", false, "4d310017abc476ba5739ca103e59e728c1987728", false, "Cullen_Rodriguez" },
                    { "ff3875bf-36c0-43f5-9f4a-35b5e62e6687", 0, "9f07b89ed71d57675e79a56174c13e5ed0747e56", "Timmy_Cole.Wunsch94@hotmail.com", false, false, null, null, "TIMMY_COLE.WUNSCH94@HOTMAIL.COM", "TIMMY_COLE", "f7d963c5d74a65a4ebddec07f5040d7e0fa83089", "721.624.6847 x9099", true, "fbe7f4c1491f511275a03750f596b21d23e2d88a", false, "Timmy_Cole" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_role_claims_role_id",
                table: "aspnet_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnet_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_claims_user_id",
                table: "aspnet_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_logins_user_id",
                table: "aspnet_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_aspnet_user_roles_role_id",
                table: "aspnet_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "aspnet_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "aspnet_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_tasks_user_id",
                table: "user_tasks",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnet_role_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_claims");

            migrationBuilder.DropTable(
                name: "aspnet_user_logins");

            migrationBuilder.DropTable(
                name: "aspnet_user_roles");

            migrationBuilder.DropTable(
                name: "aspnet_user_tokens");

            migrationBuilder.DropTable(
                name: "user_tasks");

            migrationBuilder.DropTable(
                name: "aspnet_roles");

            migrationBuilder.DropTable(
                name: "aspnet_users");
        }
    }
}
