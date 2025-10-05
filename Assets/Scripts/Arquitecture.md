# Arquitectura de Scripts

## Estructura de Carpetas

```
Assets/Scripts/
│
├── Player/                    # Funcionalidad relacionada al jugador
│   ├── PlayerController.cs    # Movimiento del personaje, cámara, salto
│   └── PlayerInteraction.cs   # Detección de objetos, agarrar, sistema de interacción
│
├── Puzzle/                    # Mecánicas del juego de rompecabezas
│   ├── PuzzleManager.cs       # Lógica de completado del puzzle, apertura del ataúd
│   └── BaseJarron.cs          # Bases de colocación con coincidencia de jeroglíficos
│
├── Items/                     # Objetos interactivos del juego
│   ├── JarronEgipcio.cs       # Jarrones egipcios con tipos de jeroglíficos
│   └── ObjetoAgarrable.cs     # Clase base para objetos agarrables
│
├── Interfaces/                # Definiciones de interfaces
│   └── IInteractuable.cs      # Contrato para objetos interactuables
│
└── UI/                        # Controladores de interfaz de usuario
    └── LinternaController.cs  # Activación de linterna y efectos
```

## Relaciones entre Componentes

```
┌─────────────────────────────────────────────────────────────────┐
│                      FLUJO DEL JUEGO                             │
└─────────────────────────────────────────────────────────────────┘

    ┌──────────────────┐
    │     JUGADOR      │
    │   PlayerController│
    └────────┬─────────┘
             │
             │ controla
             ▼
    ┌──────────────────┐        implementa      ┌─────────────────┐
    │ PlayerInteraction├───────────usa─────────►│ IInteractuable  │
    └────────┬─────────┘                         └────────┬────────┘
             │                                            │
             │ detecta y agarra                           │
             │                                            │ implementado por
             ▼                                            │
    ┌──────────────────┐                                 │
    │ ObjetoAgarrable  │◄────────────────────────────────┤
    └────────┬─────────┘                                 │
             │                                            │
             │ extiende                                   │
             ▼                                            │
    ┌──────────────────┐                                 │
    │ JarronEgipcio    │◄────────────────────────────────┘
    └────────┬─────────┘
             │
             │ colocado en
             ▼
    ┌──────────────────┐
    │   BaseJarron     │
    └────────┬─────────┘
             │
             │ notifica
             ▼
    ┌──────────────────┐
    │  PuzzleManager   │  ─────► Completa el puzzle
    └──────────────────┘         Abre el ataúd
                                 Genera el breaker


┌─────────────────────────────────────────────────────────────────┐
│                      SISTEMA UI                                  │
└─────────────────────────────────────────────────────────────────┘

    ┌──────────────────┐
    │ LinternaController│ ───► Activa/desactiva linterna
    └──────────────────┘       Efectos visuales
                               Mensajes UI
```

## Responsabilidades de Clases

### Sistema de Jugador
- **PlayerController**: Movimiento en primera persona (WASD), correr (Shift), saltar, vista con mouse
- **PlayerInteraction**: Detección por raycast, interacción con objetos (tecla E), agarrar/soltar objetos

### Sistema de Puzzle
- **PuzzleManager**: Monitorea colocación de jarrones, activa completado del puzzle, anima apertura del ataúd
- **BaseJarron**: Define puntos de colocación, valida coincidencia de jeroglíficos

### Sistema de Ítems
- **ObjetoAgarrable**: Objeto físico base con Rigidbody, puede ser recogido
- **JarronEgipcio**: Jarrón egipcio con tipo de jeroglífico (Ankh, Ojo, Escarabajo, Pirámide, Halcón)

### Capa de Interfaces
- **IInteractuable**: Interfaz estándar para todos los objetos interactivos

### Sistema UI
- **LinternaController**: Linterna encendido/apagado, efectos de parpadeo, mensajes de estado

## Flujo de Datos

```
Entrada Usuario ──► PlayerController ──► Movimiento del Personaje
                 │
                 └─► PlayerInteraction ──► Detección Raycast
                                         │
                                         ├─► IInteractuable.Interactuar()
                                         │
                                         └─► ObjetoAgarrable
                                             │
                                             └─► JarronEgipcio
                                                 │
                                                 └─► BaseJarron (¿coincide jeroglífico?)
                                                     │
                                                     └─► PuzzleManager.VerificarPuzzleCompletado()
                                                         │
                                                         └─► Completar Puzzle
                                                             ├─► Abrir Ataúd
                                                             └─► Generar Breaker
```

## Patrones de Diseño Principales

1. **Patrón de Interfaz**: `IInteractuable` permite interacción polimórfica con diferentes tipos de objetos
2. **Patrón de Componente**: Arquitectura basada en componentes de Unity para comportamientos modulares
3. **Patrón Observador**: `JarronEgipcio` notifica a `PuzzleManager` cuando se coloca correctamente
4. **Patrón de Estado**: Los objetos rastrean su estado (agarrado, colocado, puzzle completado)

## Dependencias

- **Unity Engine**: Funcionalidad principal del motor de juego
- **TextMeshPro**: Renderizado de texto UI
- **Unity Physics**: Rigidbody, Colliders, Raycasting
