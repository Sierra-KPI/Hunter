# Hunter

<p align="center">
  <img src="/docs/HunterHeader.png" data-canonical-src="/docs/HunterHeader.png"/>
</p>

## Table of Contents

- [Description](#description)
- [Badges](#badges)
- [Contributing](#contributing)
- [License](#license)

### Description

Steering behaviors aim to help autonomous characters move in a realistic manner, by using simple forces that are combined to produce life-like, improvisational navigation around the characters' environment. They are not based on complex strategies involving path planning or global calculations, but instead use local information, such as neighbors' forces. This makes them simple to understand and implement, but still able to produce very complex movement patterns

## Badges

[![Theme](https://img.shields.io/badge/Theme-GameDev-blueviolet)](https://img.shields.io/badge/Theme-GameDev-blueviolet)
[![Game](https://img.shields.io/badge/Game-Hunter-blueviolet)](https://img.shields.io/badge/Game-Hunter-blueviolet)

---

## Example

```csharp
private List<Entity> GetAllEntities()
{
    List<Entity> allEntities = new List<Entity>();

    allEntities.AddRange(_players);
    allEntities.AddRange(_food);

    return allEntities;
}

private void SpawnFood()
{
    for (var i = 0; i < FoodCount; i++)
    {
        Food food = new Food(GetRandomPosition());

        _food.Add(food);
        Board.AddEntityToBoard(food);
    }
}

public Player AddPlayer()
{
    Player player = new Player(GetRandomPosition());

    _players.Add(player);
    Board.AddEntityToBoard(player);

    return player;
}
```

---

## Pictures

[![Picture1](/docs/Hunter.png)](/docs/Hunter.png)

---

## Contributing

> To get started...

### Step 1

- 🍴 Fork this repo!

### Step 2

- **HACK AWAY!** 🔨🔨🔨

---

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

- **[MIT license](http://opensource.org/licenses/mit-license.php)**
- Copyright 2021 © <a href="https://github.com/Sierra-KPI" target="_blank">Sierra</a>.
