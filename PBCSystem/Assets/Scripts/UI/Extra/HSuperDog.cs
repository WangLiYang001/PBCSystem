using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperDog;
using System;
using System.IO;

public class HSuperDog : HSingleTonMono<HSuperDog>
{
    private string vendorCodeString =
        "rjUqp5WnFRb1czeFN5i+BCkmJSm31iFZitm7lfOvSxbOgNM8SctXy/cvM467fvfUyo/X/XvOab60gUHetjotgz3nU3kZ6t/jU3x4bZGAhkN1huEW6k41v32XV6BlNCx9II+xIwPsPXNrAZNKi3D8W0GngoLYTEH1jdn/HPaIznMd9GGuf+8c3PiofaFuMWhFdbtTdyrUi6viNShcs708BykIsMUJtVVnT6Y49qI44MeY0zycZuE6Ez2fymo8ZBespvF14ZlLFZJ/Y2yz/iFX8LDLXV518UhtcDYOdiaCHB6nzoYyHgjTUI2f6mkpXYodXeUx8HpE6Q9sY3LV1ffATyBj3sT5OuGYNVGd9lqlfuVsepXpwczXKvopAL+A0AS79UXuh5mF9SrhjoG52JeNM7ZW83qesEX5Ecj9yttWNyOoOnVYWfJUhzDTK/17DhiRlWGhWvEn6E60k6BjMnwIZUNtNBePsbCG96duw5pmquHtU8TPkJ7xvMaupfekMJJBdG5og31LkYWbv9GqA4ndHs6/OVElkl++whkC/SIpxjUgWr70pHG5ceJKltg5EuQrqu3CFIW4QreCqexy8SakwwrUkNCJROLIyNhNpCTGa0Ti9oJKANR5adQW/qnv/47P83lem0Tf5WdKiKrld28LQxkwhxntinpga3HtEfzVDSVu1dMdhgxcRTA6Kk+SvhA+6Gyczl83aGfZkY4mgBOriqp7vs5qaSc+y+RcMu5w5lfq0z0HxCq+dyuOuMSOzmJ8ZryCSEkwgaCo7bNL0wafgP2JGluUwP/DRnjhtXILdO6RA28ZMu0HMBVoJ/X7+WayL01sFJwouwRvZSUrc7kf8R91HCG9+zA1ppwdGBo8T8oCIuJNZR7wDAxUCumZjUSOY9LIbUrCmYdn5JXxVjdW7tPRoo+THMbP1HLpR/G979vBsZA73jQLGrHI9mCouBOD8jdc/SS5TgOlGhjVd6paAQ==";

    Dog _dog;
    public DogStatus CheckFeatureID(int featureID)
    {
        DogFeature feature = new DogFeature(featureID);
        _dog = new Dog(feature);
        DogStatus status = _dog.Login(vendorCodeString);
        return status;
    }

    private void OnApplicationQuit()
    {
        if (_dog != null)
        {
            if (_dog.IsLoggedIn())
                _dog.Logout();
        }
    }
}
