# Desarrollo2 - Tarea 1: Puzzle del Museo Egipcio

Un juego de rompecabezas/escape room en primera persona desarrollado en Unity donde los jugadores exploran un museo egipcio y resuelven puzzles basados en jeroglíficos.

## 🎮 Descripción del Juego

Los jugadores navegan a través de una sala de museo egipcio, buscando y recolectando jarrones antiguos con jeroglíficos. El objetivo es colocar cada jarrón en su pedestal correcto para desbloquear un ataúd y recuperar un interruptor (breaker).

## ✨ Características

- **Movimiento en Primera Persona**: Controles WASD con sprint (Shift) y salto (Espacio)
- **Cámara con Vista de Mouse**: Control de cámara suave con sensibilidad ajustable
- **Sistema de Interacción**: Recoger y manipular objetos usando la tecla E
- **Sistema de Linterna**: Activar linterna con tecla X, incluye efectos de parpadeo
- **Mecánicas de Puzzle**: Empareja jeroglíficos para desbloquear el puzzle
- **Objetos Basados en Física**: Manejo realista de objetos con Unity Physics

## 🎯 Jugabilidad

1. **Explorar**: Muévete por el museo usando WASD
2. **Iluminar**: Usa tu linterna (X) para ver en áreas oscuras
3. **Recolectar**: Encuentra y recoge (E) jarrones egipcios dispersos por el área
4. **Resolver**: Coloca cada jarrón en el pedestal con el jeroglífico correspondiente
5. **Completar**: Una vez que los 5 jarrones estén correctamente colocados, el ataúd se abre
6. **Recuperar**: Recoge el breaker del interior del ataúd

## 🗝️ Controles

| Tecla | Acción |
|-------|--------|
| **W/A/S/D** | Mover adelante/izquierda/atrás/derecha |
| **Shift** | Correr |
| **Espacio** | Saltar |
| **Mouse** | Mirar alrededor |
| **E** | Interactuar / Recoger / Soltar objetos |
| **X** | Activar/desactivar linterna |
| **ESC** | Desbloquear cursor |

## 📁 Estructura del Proyecto

```
Assets/Scripts/
├── Player/           # Movimiento e interacción del jugador
├── Puzzle/           # Lógica y mecánicas del puzzle
├── Items/            # Objetos interactivos del juego
├── Interfaces/       # Definiciones de interfaces
└── UI/               # Controladores de interfaz de usuario
```

Para información detallada sobre la arquitectura, ver [Arquitectura de Scripts](Assets/Scripts/Arquitecture.md).

## 🔧 Detalles Técnicos

- **Motor**: Unity 2022+
- **Física**: Unity Physics System
- **UI**: TextMeshPro
- **Input**: Unity Input System (legacy)
- **Lenguaje**: C#

## 🏺 Tipos de Jeroglíficos

El juego presenta 5 jeroglíficos egipcios:

1. **Ankh** (☥) - Símbolo de vida
2. **Ojo de Horus** (𓂀) - Protección
3. **Escarabajo** (𓆣) - Transformación
4. **Pirámide** (𓉾) - Eternidad
5. **Halcón** (𓅃) - Cielo y divinidad

## 🏗️ Arquitectura

El proyecto sigue una arquitectura modular basada en componentes:

- **Sistema de Jugador**: Maneja el controlador del personaje y la interacción
- **Sistema de Puzzle**: Gestiona la lógica del juego y condiciones de victoria
- **Sistema de Ítems**: Define objetos agarrables y jarrones
- **Capa de Interfaces**: Proporciona contratos para objetos interactuables

Consulta el [diagrama de arquitectura](Assets/Scripts/Arquitecture.md) completo para información detallada.

## 🚀 Comenzando

### Prerequisitos
- Unity 2022.3 LTS o superior
- Paquete TextMeshPro

### Instalación
1. Clona el repositorio:
   ```bash
   git clone https://github.com/yourusername/Desarrollo2_Tarea1.git
   ```
2. Abre el proyecto en Unity Hub
3. Abre la escena principal en `Assets/Scenes/`
4. Presiona Play para probar

## 📝 Desarrollo

### Rama Actual
- `feat-egypt-room` - Rama principal de desarrollo para el puzzle de la sala egipcia

### Actualizaciones Recientes
- ✅ Scripts organizados en estructura de carpetas lógica
- ✅ Mecánicas de colocación de jarrones implementadas
- ✅ Sistema de completado de puzzle añadido
- ✅ Sistema de interacción del jugador creado
- ✅ Controlador de linterna añadido

## 🤝 Contribuciones

Este es un proyecto estudiantil para el curso de Desarrollo 2.

## 📄 Licencia

Proyecto educativo - Todos los derechos reservados.

## 👨‍💻 Autor

Proyecto estudiantil para el curso de desarrollo de juegos.

---

**Nota**: Para documentación técnica sobre la arquitectura del código, relaciones entre clases y patrones de diseño, por favor consulta [Arquitecture.md](Assets/Scripts/Arquitecture.md).
