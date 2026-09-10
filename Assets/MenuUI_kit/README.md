# Dice Menu UI Kit (Unity)

Assets for the parchment-and-walnut dice settings menu. Text is intentionally not baked into images: use TextMeshPro for D4, D6, D8, D10, D12, D20, values, title and Display score.

## Import settings
- Texture Type: Sprite (2D and UI)
- Sprite Mode: Single
- Alpha Is Transparency: enabled for all files except wood_background.png
- Filter Mode: Bilinear
- Compression: None or High Quality
- Mesh Type: Full Rect

## Suggested slicing
- parchment_panel.png: Border 90 / 90 / 90 / 90, Image Type = Sliced
- number_field.png: Border 70 / 60 / 70 / 60, Image Type = Sliced
- round buttons and toggles: Image Type = Simple, Preserve Aspect enabled

## Button setup
Use Sprite Swap transition:
- Highlighted: round_button_hover.png
- Pressed: round_button_pressed.png
- Disabled: round_button_disabled.png
Place icon_plus.png or icon_minus.png as a child Image.

## Included
Panels, four round-button states, number field, on/off toggle, plus/minus/check icons, divider, brass stud, and a menu reference image.


## Added in v2
+- `divider_line_long.png`: full-width row divider.
+- `divider_line_short.png`: compact divider.
+- `divider_line_faded.png`: divider with fading ends.
+- `ornament_diamond.png`: separate central diamond.
+- `title_ornament.png`: two lines and diamonds around the DICE title.
+- `panel_shadow.png`: optional soft shadow behind the parchment.
+- `FONT_GUIDE.md`: exact typography recommendation.
+