﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSelectorScript : MonoBehaviour
{

    public List<Mesh> listOfMeshes = new List<Mesh>(Enum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length);
    public List<MeshFilter> listOfMeshFilters = new List<MeshFilter>(Enum.GetNames(typeof(PlayerManager.CharacterClassesEnum)).Length);

    Mesh currentMesh;
    MeshFilter currentMeshFilter;

    void Start()
    {
        currentMesh = GetComponent<Mesh>();
        currentMeshFilter = GetComponent<MeshFilter>();
    }

    public void SetMask(PlayerManager.CharacterClassesEnum selectedClass)
    {
        foreach (Transform kiddo in transform)
        {
            kiddo.gameObject.SetActive(false);

        }
        transform.GetChild((int)selectedClass).gameObject.SetActive(true);

        //currentMesh = listOfMeshes[(int)selectedClass];
        //currentMeshFilter = listOfMeshFilters[(int)selectedClass];
    }
}