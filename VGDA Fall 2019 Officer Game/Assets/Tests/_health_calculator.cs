﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class _health_calculator
    {
        [Test]
        public void Set_HealthValue()
        {
            //Act
            Health playerHealth = new Health();
            playerHealth.HealthTotal = 100;
            
            //ASSERT
            Assert.AreEqual(100, playerHealth.HealthTotal);
        }

        [Test]
        public void _try_to_set_health_to_zero()
        {
            Health playerHealth = new Health();
            Health.OnDeath += _try_to_invoke_health_event;
            
            BeamTest newBeam = new BeamTest();
             
            newBeam.DealDamage(playerHealth);
            newBeam.DealDamage(playerHealth);

            Assert.AreEqual(0, playerHealth.HealthTotal);
            
            Health.OnDeath += _try_to_invoke_health_event;

        }
        
        [Test]
        public void _try_to_set_health_to_50()
        {
            Health playerHealth = new Health();
            Health.OnDeath += _try_to_invoke_health_event;
            
            BeamTest newBeam = new BeamTest();
             
            newBeam.DealDamage(playerHealth);

            Assert.AreEqual(50, playerHealth.HealthTotal);
            
            Health.OnDeath += _try_to_invoke_health_event;

        }

        [Test]
        public void _destroy_the_correct_object()
        {
            Health.OnDeath += _destroy_object_when_dead;

            Health playerHealth = new Health();
            Health enemy1 = new Health();
            BeamTest aBeam = new BeamTest(100);
            
            aBeam.DealDamage(enemy1);
            
            Assert.AreEqual(0, enemy1.HealthTotal);
            
            Health.OnDeath -= _destroy_object_when_dead;
        }

        public void _destroy_object_when_dead(Health dead)
        {
            Debug.Log("Handle death");
        }
        

        public void _try_to_invoke_health_event(Health dead)
        {
            Debug.Log("The health is zero do something");
        }
    }

}


