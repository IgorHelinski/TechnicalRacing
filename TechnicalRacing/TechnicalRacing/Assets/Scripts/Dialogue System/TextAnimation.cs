using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public float frequency;

    TextMeshProUGUI text;

    Mesh mesh;
    Vector3[] vertecies;

    private void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertecies = mesh.vertices;

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = text.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertecies[index] += offset;
            vertecies[index + 1] += offset;
            vertecies[index + 2] += offset;
            vertecies[index + 3] += offset;
        }

        mesh.vertices = vertecies;
        text.canvasRenderer.SetMesh(mesh);
        
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * frequency), 0);
    }
}
