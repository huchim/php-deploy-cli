# PHP Deploy Tool

Esta es una herramienta simple que sirve para comprar una lista de archivos entre una carpeta local (cliente) y una carpeta remota (servidor).

Para poder hacer la comparación se requiere de [php-deploy-server](https://github.com/huchim/php-deploy-server) instalado.

## Manera de usar.

```bash
phpdeploy --url http://localhost:8000 -a sao -s development -r H:\git_projects3\avance-obra\api --output cambios.zip
```

Las opciones son definidas en el archivo [`CliOptions/DeployOptions.cs`](/CliOptions/DeployOptions.cs)

El comando anterior generará un archivo comprimida con los archivos que sufrieron cambios.

## Proyecto de prueba.

Este proyecto es de prueba, y es porque necesito una manera sencilla de subir los cambios del proyecto al servidor. Usa NET Framework, así que por el momento no funciona más que en Windows.

### TODO

* [x] Crear una aplicación de consola que acepta parámetros.
* [x] Crear un listado de archivos con su clave de contenido.
* [x] Enviar el listado al servidor y recibir la diferencia.
* [ ] Seleccionar únicamente los archivos a actualziar y generar un archivo ZIP.
* [ ] Enviar directamente al servidor el ZIP
* [ ] Crear otros clientes.