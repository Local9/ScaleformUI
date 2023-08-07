Blip = setmetatable({}, Blip)
Blip.__index = Blip
Blip.__call = function()
  return "Blip"
end

function Blip.New(sprite, color, scale, alpha, text, isShortRange)
  local _blip = {
    Sprite = sprite or 1,
    Color = color or 0,
    Scale = scale or 1.0,
    Alpha = alpha or 255,
    Name = text or "",
    ShortRange = isShortRange or false
  }
  return setmetatable(_blip, Blip)
end

function Blip.Simple(sprite, position, color, text)
  local _blip = Blip.New(sprite, color, 1.0, 255, text)
  _blip.Position = position
  return _blip
end
