# Fuxion source code

Each project scope has this structure/layers:
 - Domain: Business logic
 - Application: Provide access to domain layer
 - Infrastructure: Provide concrete implementations of domain artefacts
 - Projection: Transform domain output to more readable data schemas
 - Query: Provide access to projection results
 - Presentation: User interface components
