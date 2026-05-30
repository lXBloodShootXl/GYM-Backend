using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RRHH.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    id_asistencia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.id_asistencia);
                });

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    id_auditoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tabla = table.Column<string>(type: "text", nullable: false),
                    id_registro = table.Column<int>(type: "integer", nullable: false),
                    accion = table.Column<string>(type: "text", nullable: false),
                    datos_anteriores = table.Column<string>(type: "text", nullable: false),
                    datos_nuevos = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.id_auditoria);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Cargo_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Cargo_Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Correos",
                columns: table => new
                {
                    id_correo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    correo = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correos", x => x.id_correo);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    id_inventario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.id_inventario);
                });

            migrationBuilder.CreateTable(
                name: "Membresias",
                columns: table => new
                {
                    id_membresia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    duracion = table.Column<int>(type: "integer", nullable: false),
                    precio = table.Column<decimal>(type: "numeric", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membresias", x => x.id_membresia);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ci = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido_p = table.Column<string>(type: "text", nullable: true),
                    apellido_m = table.Column<string>(type: "text", nullable: true),
                    sexo = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    hashhuella = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.id_persona);
                });

            migrationBuilder.CreateTable(
                name: "Salario",
                columns: table => new
                {
                    Salario_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Salarioo = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salario", x => x.Salario_Id);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    id_telefono = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    telf = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.id_telefono);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    id_turno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    hora_inicio = table.Column<string>(type: "text", nullable: false),
                    hora_fin = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.id_turno);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    precio = table.Column<decimal>(type: "numeric", nullable: false),
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categorias",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_persona = table.Column<int>(type: "integer", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    pwd = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "Personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    id_empleado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    pwd = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    id_persona = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.id_empleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "Personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaAsistencias",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "integer", nullable: false),
                    id_asistencia = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaAsistencias", x => new { x.id_persona, x.id_asistencia });
                    table.ForeignKey(
                        name: "FK_PersonaAsistencias_Asistencias_id_asistencia",
                        column: x => x.id_asistencia,
                        principalTable: "Asistencias",
                        principalColumn: "id_asistencia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaAsistencias_Personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "Personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaCorreos",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "integer", nullable: false),
                    id_correo = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    correo = table.Column<string>(type: "text", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaCorreos", x => new { x.id_persona, x.id_correo, x.fecha_inicio });
                    table.ForeignKey(
                        name: "FK_PersonaCorreos_Correos_id_correo",
                        column: x => x.id_correo,
                        principalTable: "Correos",
                        principalColumn: "id_correo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaCorreos_Personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "Personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoSalario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Salario = table.Column<int>(type: "integer", nullable: false),
                    Id_Cargo = table.Column<int>(type: "integer", nullable: false),
                    Fecha_Inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Fecha_Fin = table.Column<DateOnly>(type: "date", nullable: false),
                    Salario_Id = table.Column<int>(type: "integer", nullable: false),
                    Cargo_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoSalario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoSalario_Cargo_Cargo_Id",
                        column: x => x.Cargo_Id,
                        principalTable: "Cargo",
                        principalColumn: "Cargo_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoSalario_Salario_Salario_Id",
                        column: x => x.Salario_Id,
                        principalTable: "Salario",
                        principalColumn: "Salario_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaTelefonos",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "integer", nullable: false),
                    id_telefono = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    ci = table.Column<string>(type: "text", nullable: false),
                    telf = table.Column<string>(type: "text", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaTelefonos", x => new { x.id_persona, x.id_telefono, x.fecha_inicio });
                    table.ForeignKey(
                        name: "FK_PersonaTelefonos_Personas_id_persona",
                        column: x => x.id_persona,
                        principalTable: "Personas",
                        principalColumn: "id_persona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaTelefonos_Telefonos_id_telefono",
                        column: x => x.id_telefono,
                        principalTable: "Telefonos",
                        principalColumn: "id_telefono",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    id_inventario = table.Column<int>(type: "integer", nullable: false),
                    id_producto = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => new { x.id_inventario, x.id_producto });
                    table.ForeignKey(
                        name: "FK_Stocks_Inventarios_id_inventario",
                        column: x => x.id_inventario,
                        principalTable: "Inventarios",
                        principalColumn: "id_inventario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Productos_id_producto",
                        column: x => x.id_producto,
                        principalTable: "Productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suscripciones",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "integer", nullable: false),
                    id_membresia = table.Column<int>(type: "integer", nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripciones", x => new { x.id_cliente, x.id_membresia, x.fecha_inicio });
                    table.ForeignKey(
                        name: "FK_Suscripciones_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suscripciones_Membresias_id_membresia",
                        column: x => x.id_membresia,
                        principalTable: "Membresias",
                        principalColumn: "id_membresia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_Cargo = table.Column<int>(type: "integer", nullable: false),
                    Id_Empleado = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    FechaIncio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Cargo_Id = table.Column<int>(type: "integer", nullable: false),
                    id_Empleado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoCargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpleadoCargo_Cargo_Cargo_Id",
                        column: x => x.Cargo_Id,
                        principalTable: "Cargo",
                        principalColumn: "Cargo_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoCargo_Empleados_id_Empleado",
                        column: x => x.id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "id_empleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoTurnos",
                columns: table => new
                {
                    id_empleado = table.Column<int>(type: "integer", nullable: false),
                    id_turno = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoTurnos", x => new { x.id_empleado, x.id_turno });
                    table.ForeignKey(
                        name: "FK_EmpleadoTurnos_Empleados_id_empleado",
                        column: x => x.id_empleado,
                        principalTable: "Empleados",
                        principalColumn: "id_empleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoTurnos_Turnos_id_turno",
                        column: x => x.id_turno,
                        principalTable: "Turnos",
                        principalColumn: "id_turno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    id_empleado = table.Column<int>(type: "integer", nullable: false),
                    id_cliente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_id_empleado",
                        column: x => x.id_empleado,
                        principalTable: "Empleados",
                        principalColumn: "id_empleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "integer", nullable: false),
                    id_producto = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => new { x.id_venta, x.id_producto });
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_id_producto",
                        column: x => x.id_producto,
                        principalTable: "Productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_id_venta",
                        column: x => x.id_venta,
                        principalTable: "Ventas",
                        principalColumn: "id_venta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_fecha",
                table: "Asistencias",
                column: "fecha",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CargoSalario_Cargo_Id",
                table: "CargoSalario",
                column: "Cargo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSalario_Salario_Id",
                table: "CargoSalario",
                column: "Salario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_id_persona",
                table: "Clientes",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_Correos_correo",
                table: "Correos",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_id_producto",
                table: "DetalleVentas",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCargo_Cargo_Id",
                table: "EmpleadoCargo",
                column: "Cargo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCargo_id_Empleado",
                table: "EmpleadoCargo",
                column: "id_Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_id_persona",
                table: "Empleados",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoTurnos_id_turno",
                table: "EmpleadoTurnos",
                column: "id_turno");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaAsistencias_id_asistencia",
                table: "PersonaAsistencias",
                column: "id_asistencia");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaAsistencias_id_persona_id_asistencia",
                table: "PersonaAsistencias",
                columns: new[] { "id_persona", "id_asistencia" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaCorreos_ci_correo_fecha_inicio",
                table: "PersonaCorreos",
                columns: new[] { "ci", "correo", "fecha_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaCorreos_id_correo",
                table: "PersonaCorreos",
                column: "id_correo");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_ci",
                table: "Personas",
                column: "ci",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_hashhuella",
                table: "Personas",
                column: "hashhuella",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTelefonos_ci_telf_fecha_inicio",
                table: "PersonaTelefonos",
                columns: new[] { "ci", "telf", "fecha_inicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTelefonos_id_telefono",
                table: "PersonaTelefonos",
                column: "id_telefono");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_id_categoria",
                table: "Productos",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_id_producto",
                table: "Stocks",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_id_membresia",
                table: "Suscripciones",
                column: "id_membresia");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_telf",
                table: "Telefonos",
                column: "telf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_codigo",
                table: "Turnos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_cliente",
                table: "Ventas",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_id_empleado",
                table: "Ventas",
                column: "id_empleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "CargoSalario");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "EmpleadoCargo");

            migrationBuilder.DropTable(
                name: "EmpleadoTurnos");

            migrationBuilder.DropTable(
                name: "PersonaAsistencias");

            migrationBuilder.DropTable(
                name: "PersonaCorreos");

            migrationBuilder.DropTable(
                name: "PersonaTelefonos");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Suscripciones");

            migrationBuilder.DropTable(
                name: "Salario");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Correos");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Membresias");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
