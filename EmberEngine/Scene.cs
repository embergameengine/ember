using EmberEngine.Components;
using Newtonsoft.Json;
using Silk.NET.OpenGL;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Numerics;
using System.Reflection;
using Jitter;
using Jitter.Collision;

namespace EmberEngine
{
    public class Scene
    {
        public string name;
        public List<GameObject> objects;

        public Vector4 backgroundColor;
        [JsonProperty]
        public bool useBgColor;

        [JsonIgnore]
        private ApplicationBase application;

        [JsonIgnore]
        private GL _gl;

        internal World physicsWorld;

        public Camera GetCamera()
        {
            try
            {
                return FindObjectsOfType<Camera>()[0];
            } catch
            {
                return null;
            }
        }

        public void AddObject(GameObject obj)
        {
            objects.Add(obj);
        }

        public Scene(string name)
        {
            this.name = name;

            objects = new List<GameObject>();

            useBgColor = false;

            physicsWorld = new World(new CollisionSystemSAP());

            physicsWorld.Gravity = new Jitter.LinearMath.JVector(0, 1, 0);
        }

        public Scene(string name, Vector4 backgroundColor)
        {
            this.name = name;

            objects = new List<GameObject>();

            this.backgroundColor = backgroundColor;

            useBgColor = true;

            _gl = Globals.application._gl;

            physicsWorld = new World(new CollisionSystemSAP());

            physicsWorld.Gravity = new Jitter.LinearMath.JVector(0, 1, 0);
        }

        public Scene()
        {
            _gl = Globals.application._gl;

            physicsWorld = new World(new CollisionSystemSAP());

            physicsWorld.Gravity = new Jitter.LinearMath.JVector(0, -5, 0);
        }

        public virtual void Update(double dt)
        {
            physicsWorld.Step((float)1 / 100, false);
            foreach (GameObject obj in objects)
            {
                obj.Update(dt);
            }

            
        }

        public void Render()
        {
            if (GetCamera() == null)
            {
                GameObject obj = new GameObject("Camera");
                Camera camera = new Camera(new Vector3(0, 0, 0), Globals.application._window);

                obj.AddComponent(camera);

                AddObject(obj);
            }

            ProjectionMatrices matrices = GetCamera().CreateProjectionMatricies();
            if (useBgColor)
            {
                _gl.ClearColor(backgroundColor.X, backgroundColor.Y, backgroundColor.Z, backgroundColor.W);
            }

            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach (GameObject obj in objects)
            {
                obj.Render(matrices);
            }
        }

        public T[] FindObjectsOfType<T>() where T : Component
        {
            List<T> output = new List<T>();

            foreach (GameObject obj in objects)
            {
                if (obj.HasComponent<T>())
                {
                    output.Add(obj.GetComponent<T>());
                }
            }

            return output.ToArray();
        }

        public void Save(string name)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(this, settings);

            File.WriteAllText(name, json);
        }

        static void InterventionCallback(Func<Scene, int> callback, Scene scene)
        {
            Console.WriteLine("Intializing Objects..");
            foreach (GameObject obj in scene.objects)
            {
                obj._gl = Globals.application._gl;
                Console.WriteLine($"Loading meshes for object {obj.name}...");

                if (obj.HasComponent<MeshRenderer>())
                {
                    //Console.WriteLine("Hi");
                    obj.GetComponent<MeshRenderer>().LoadFromScene();

                }


                Console.WriteLine($"Setting up components for {obj.name}...");

                foreach (Component component in obj.components)
                {
                    component.gameObject = obj;
                    //Console.WriteLine(component.gameObject.name);

                    component._gl = Globals.application._gl;

                    //Console.WriteLine(Globals.application._gl);

                    Console.WriteLine(obj.name);

                    component.Load();
                }


            }

            callback(scene);
        }
        /*
        public static void FromFile(string name, Func<Scene, int> callback)
        {
            Thread t = new Thread(() =>
            {
                Console.WriteLine("Loading Scene");
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                Scene scene = JsonConvert.DeserializeObject<Scene>(File.ReadAllText(name), settings);


                

                InterventionCallback(callback, scene);
                
            });

            t.Start();
        }
        */

        static object JsonNodeToClass<T>(JsonNode node)
        {
            JsonObject asObject = node.AsObject();
            RemoveNodes(asObject, "$type");

            string jsonString = asObject.ToJsonString();

            Console.WriteLine(jsonString);

            object obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);



            if (obj is Transform transform)
            {
                Console.WriteLine(transform.position.Y);
            }

            return obj;
        }

        static void RemoveNodes(JsonObject jsonObject, string nodeName)
        {
            // Create a list to hold the keys of the nodes to remove
            List<string> keysToRemove = new List<string>();

            // Iterate over the properties in the JSON object
            foreach (KeyValuePair<string, JsonNode> property in jsonObject)
            {
                string key = property.Key;
                JsonNode value = property.Value;

                // If the property is a JSON object, call the method recursively
                if (value is JsonObject obj)
                {
                    RemoveNodes(obj, nodeName);
                }

                // If the property is a JSON array, iterate over its elements
                if (value is JsonArray array)
                {
                    foreach (JsonNode element in array)
                    {
                        // If the element is a JSON object, call the method recursively
                        if (element is JsonObject obj2)
                        {
                            RemoveNodes(obj2, nodeName);
                        }
                    }
                }

                // If the property name matches the node name to remove, add it to the list of keys to remove
                if (key == nodeName)
                {
                    keysToRemove.Add(key);
                }
            }

            // Remove the nodes with the specified name
            foreach (var key in keysToRemove)
            {
                jsonObject.Remove(key);
            }
        }

        static object JsonNodeToClass(JsonNode node, Type type)
        {
            JsonObject asObject = node.AsObject();
            RemoveNodes(asObject, "$type");

            string jsonString = asObject.ToJsonString();

            Console.WriteLine(jsonString);

            object obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, type);

            

            if (obj is Transform transform)
            {
                Console.WriteLine(transform.position.Y);
            }

            return obj;
        }

        public static Scene FromFile(string name, Assembly editorAsm)
        {
            /*
            Console.WriteLine("Loading Scene");
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            Scene scene = JsonConvert.DeserializeObject<Scene>(File.ReadAllText(name), settings);
            Scene sceneDupe = new Scene(scene.name);
            sceneDupe.useBgColor = scene.useBgColor;
            sceneDupe.backgroundColor = scene.backgroundColor;
            sceneDupe._gl = Globals.application._gl;

            Console.WriteLine("Intializing Objects..");
            foreach (GameObject obj in scene.objects)
            {
                GameObject objDupe = new GameObject(obj.name);
                objDupe.transform = obj.transform;
                obj._gl = Globals.application._gl;
                objDupe._gl = Globals.application._gl;
                Console.WriteLine($"Loading meshes for object {obj.name}...");

                if (obj.HasComponent<MeshRenderer>())
                {
                    //Console.WriteLine("Hi");
                    obj.GetComponent<MeshRenderer>().LoadSceneData();
                }


                Console.WriteLine($"Setting up components for {obj.name}...");

                foreach (Component component in obj.components)
                {
                    
                    component.gameObject = obj;
                    //Console.WriteLine(component.gameObject.name);

                    component._gl = Globals.application._gl;

                    //Console.WriteLine(Globals.application._gl);

                    Console.WriteLine(obj.name);

                    component.Load();

                    objDupe.AddComponent(component);
                }

                sceneDupe.AddObject(objDupe);
            }

            return sceneDupe;
            */

            Console.WriteLine(Environment.CurrentDirectory);

            string json = File.ReadAllText(name);

            JsonNode rootNode = JsonNode.Parse(json);
            Scene scene = new Scene();

            JsonNode nameNode = rootNode["name"];
            JsonNode useBgColorNode = rootNode["useBgColor"];
            JsonNode backgroundColorNode = rootNode["backgroundColor"];
            JsonNode objectsNode = rootNode["objects"]["$values"];
            JsonNode transformNode = rootNode["transform"];

            scene.name = nameNode.GetValue<string>();
            scene.useBgColor = useBgColorNode.GetValue<bool>();
            scene.backgroundColor = (Vector4)JsonNodeToClass<Vector4>(backgroundColorNode);

            List<GameObject> objects = new List<GameObject>();

            if (objectsNode is JsonArray objectsArray)
            {
                for (int i = 0; i < objectsArray.Count; i++)
                {
                    JsonNode objectNode = objectsArray[i];

                    GameObject obj = new GameObject(objectNode["name"].GetValue<string>());

                    obj.transform = (Transform)JsonNodeToClass<Transform>(objectNode["transform"]);

                    JsonArray componentsNode = objectNode["components"]["$values"].AsArray();

                    for (int x = 0; x < componentsNode.Count; x++)
                    {
                        JsonNode componentNode = componentsNode[x];
                        // Use componentNode

                        string type = componentNode["$type"].GetValue<string>().Split(", ")[0];

                        Type componentType = Type.GetType(type);

                        if (componentType == null)
                        {
                            componentType = editorAsm.GetType(type);

                            if (componentType == null)
                            {
                                throw new Exception("Invalid Component Type " + type);
                            }
                        }

                        //Console.WriteLine(componentType.Name);

                        if (componentType.BaseType == typeof(Component))
                        {
                            Component rawComponentInstance = Activator.CreateInstance(componentType) as Component;
                            Component component = (Component)Convert.ChangeType(rawComponentInstance, componentType);

                            foreach (KeyValuePair<string, JsonNode> node in componentNode.AsObject())
                            {
                                string key = node.Key;
                                JsonNode value = node.Value;

                                //Console.WriteLine(key);

                                if (key == "$type")
                                {
                                    continue;
                                }

                                PropertyInfo property = componentType.GetProperty(key);

                                //Console.WriteLine(componentType.GetProperties().ToList().Contains(property));

                                if (property != null)
                                {
                                    if (property.CanWrite)
                                    {
                                        Type propertyType = property.PropertyType;
                                        object propertyValue;

                                        switch (propertyType.Name)
                                        {
                                            case nameof(String):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<string>();
                                                break;

                                            case nameof(Single):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<float>();
                                                break;

                                            case nameof(Boolean):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<bool>();
                                                break;

                                            case nameof(Double):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<double>();
                                                break;

                                            case nameof(Int32):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<int>();
                                                break;

                                            default:
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = Convert.ChangeType(JsonNodeToClass(value, propertyType), propertyType);
                                                break;
                                        }

                                        try
                                        {
                                            
                                            property.SetValue(component, propertyValue, null);
                                        }
                                        catch (ArgumentException)
                                        {
                                            Console.WriteLine($"Unable to set property {key} due to type mismatch");
                                        }
                                        catch (TargetException)
                                        {
                                            Console.WriteLine($"Unable to set property {key} on null object");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Property {key} is read-only");
                                    }
                                }
                                else
                                {
                                    FieldInfo field = componentType.GetField(key);

                                    //Console.WriteLine(componentType.GetFields().ToList().Contains(field));

                                    if (field != null)
                                    {
                                        Type fieldType = field.FieldType;
                                        object fieldValue;

                                        switch (fieldType.Name)
                                        {
                                            case nameof(String):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<string>();
                                                break;

                                            case nameof(Single):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<float>();
                                                break;

                                            case nameof(Boolean):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<bool>();
                                                break;

                                            case nameof(Double):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<double>();
                                                break;

                                            case nameof(Int32):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<int>();
                                                break;

                                            default:
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = JsonNodeToClass(value, fieldType);
                                                break;
                                        }

                                        try
                                        {
                                            field.SetValue(component, fieldValue);

                                            if (field.GetValue(component) == null)
                                            {
                                                //Console.WriteLine("Something aint right");
                                                throw new Exception("Component is null");
                                            }
                                        }
                                        catch (ArgumentException)
                                        {
                                            Console.WriteLine($"Unable to set field {key} due to type mismatch");
                                        }
                                        catch (FieldAccessException)
                                        {
                                            Console.WriteLine($"Unable to set field {key} on null object");
                                        }
                                    }
                                }
                            }

                            // loop over

                            component.gameObject = obj;
                            component._gl = Globals.application._gl;
                            component.transform = obj.transform;

                            obj.AddComponent(component, false);

                            component.LoadFromScene();

                            component.Load();

                            objects.Add(obj);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Invalid objects node in scene");
            }

            scene.objects = objects;

            return scene;
        }

        public static Scene FromFile(string name)
        {
            /*
            Console.WriteLine("Loading Scene");
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            Scene scene = JsonConvert.DeserializeObject<Scene>(File.ReadAllText(name), settings);
            Scene sceneDupe = new Scene(scene.name);
            sceneDupe.useBgColor = scene.useBgColor;
            sceneDupe.backgroundColor = scene.backgroundColor;
            sceneDupe._gl = Globals.application._gl;

            Console.WriteLine("Intializing Objects..");
            foreach (GameObject obj in scene.objects)
            {
                GameObject objDupe = new GameObject(obj.name);
                objDupe.transform = obj.transform;
                obj._gl = Globals.application._gl;
                objDupe._gl = Globals.application._gl;
                Console.WriteLine($"Loading meshes for object {obj.name}...");

                if (obj.HasComponent<MeshRenderer>())
                {
                    //Console.WriteLine("Hi");
                    obj.GetComponent<MeshRenderer>().LoadSceneData();
                }


                Console.WriteLine($"Setting up components for {obj.name}...");

                foreach (Component component in obj.components)
                {
                    
                    component.gameObject = obj;
                    //Console.WriteLine(component.gameObject.name);

                    component._gl = Globals.application._gl;

                    //Console.WriteLine(Globals.application._gl);

                    Console.WriteLine(obj.name);

                    component.Load();

                    objDupe.AddComponent(component);
                }

                sceneDupe.AddObject(objDupe);
            }

            return sceneDupe;
            */

            Console.WriteLine(Environment.CurrentDirectory);

            string json = File.ReadAllText(name);

            JsonNode rootNode = JsonNode.Parse(json);
            Scene scene = new Scene();

            JsonNode nameNode = rootNode["name"];
            JsonNode useBgColorNode = rootNode["useBgColor"];
            JsonNode backgroundColorNode = rootNode["backgroundColor"];
            JsonNode objectsNode = rootNode["objects"]["$values"];
            JsonNode transformNode = rootNode["transform"];

            scene.name = nameNode.GetValue<string>();
            scene.useBgColor = useBgColorNode.GetValue<bool>();
            scene.backgroundColor = (Vector4)JsonNodeToClass<Vector4>(backgroundColorNode);

            List<GameObject> objects = new List<GameObject>();

            if (objectsNode is JsonArray objectsArray)
            {
                for (int i = 0; i < objectsArray.Count; i++)
                {
                    JsonNode objectNode = objectsArray[i];

                    GameObject obj = new GameObject(objectNode["name"].GetValue<string>());

                    obj.transform = (Transform)JsonNodeToClass<Transform>(objectNode["transform"]);

                    JsonArray componentsNode = objectNode["components"]["$values"].AsArray();

                    for (int x = 0; x < componentsNode.Count; x++)
                    {
                        JsonNode componentNode = componentsNode[x];
                        // Use componentNode

                        string type = componentNode["$type"].GetValue<string>().Split(", ")[0];

                        Type componentType = Type.GetType(type);

                        

                        //Console.WriteLine(componentType.Name);

                        if (componentType.BaseType == typeof(Component))
                        {
                            Component rawComponentInstance = Activator.CreateInstance(componentType) as Component;
                            Component component = (Component)Convert.ChangeType(rawComponentInstance, componentType);

                            foreach (KeyValuePair<string, JsonNode> node in componentNode.AsObject())
                            {
                                string key = node.Key;
                                JsonNode value = node.Value;

                                //Console.WriteLine(key);

                                if (key == "$type")
                                {
                                    continue;
                                }

                                PropertyInfo property = componentType.GetProperty(key);

                                //Console.WriteLine(componentType.GetProperties().ToList().Contains(property));

                                if (property != null)
                                {
                                    if (property.CanWrite)
                                    {
                                        Type propertyType = property.PropertyType;
                                        object propertyValue;

                                        switch (propertyType.Name)
                                        {
                                            case nameof(String):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<string>();
                                                break;

                                            case nameof(Single):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<float>();
                                                break;

                                            case nameof(Boolean):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<bool>();
                                                break;

                                            case nameof(Double):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<double>();
                                                break;

                                            case nameof(Int32):
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = value.GetValue<int>();
                                                break;

                                            default:
                                                if (value == null)
                                                {
                                                    propertyValue = null;
                                                    break;
                                                }
                                                propertyValue = Convert.ChangeType(JsonNodeToClass(value, propertyType), propertyType);
                                                break;
                                        }

                                        try
                                        {

                                            property.SetValue(component, propertyValue, null);
                                        }
                                        catch (ArgumentException)
                                        {
                                            Console.WriteLine($"Unable to set property {key} due to type mismatch");
                                        }
                                        catch (TargetException)
                                        {
                                            Console.WriteLine($"Unable to set property {key} on null object");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Property {key} is read-only");
                                    }
                                }
                                else
                                {
                                    FieldInfo field = componentType.GetField(key);

                                    //Console.WriteLine(componentType.GetFields().ToList().Contains(field));

                                    if (field != null)
                                    {
                                        Type fieldType = field.FieldType;
                                        object fieldValue;

                                        switch (fieldType.Name)
                                        {
                                            case nameof(String):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<string>();
                                                break;

                                            case nameof(Single):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<float>();
                                                break;

                                            case nameof(Boolean):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<bool>();
                                                break;

                                            case nameof(Double):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<double>();
                                                break;

                                            case nameof(Int32):
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = value.GetValue<int>();
                                                break;

                                            default:
                                                if (value == null)
                                                {
                                                    fieldValue = null;
                                                    break;
                                                }
                                                fieldValue = JsonNodeToClass(value, fieldType);
                                                break;
                                        }

                                        try
                                        {
                                            field.SetValue(component, fieldValue);

                                            if (field.GetValue(component) == null)
                                            {
                                                //Console.WriteLine("Something aint right");
                                                throw new Exception("Component is null");
                                            }
                                        }
                                        catch (ArgumentException)
                                        {
                                            Console.WriteLine($"Unable to set field {key} due to type mismatch");
                                        }
                                        catch (FieldAccessException)
                                        {
                                            Console.WriteLine($"Unable to set field {key} on null object");
                                        }
                                    }
                                }
                            }

                            // loop over

                            component.gameObject = obj;
                            component._gl = Globals.application._gl;
                            component.transform = obj.transform;

                            obj.AddComponent(component, false);

                            component.LoadFromScene();

                            component.Load();

                            objects.Add(obj);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Invalid objects node in scene");
            }

            scene.objects = objects;

            return scene;
        }
    }
}
