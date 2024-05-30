-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: May 29, 2024 at 06:13 PM
-- Server version: 5.7.24
-- PHP Version: 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unity`
--

-- --------------------------------------------------------

--
-- Table structure for table `games`
--

DROP TABLE IF EXISTS games, players;

CREATE TABLE `games` (
  `id` int(11) NOT NULL,
  `user_ID` int(11) NOT NULL,
  `date` date NOT NULL,
  `concentrationPoints` int(11) DEFAULT NULL,
  `concentrationErrors` int(11) DEFAULT NULL,
  `concentrationScore` int(11) DEFAULT NULL,
  `destinationPoints` int(11) DEFAULT NULL,
  `destinationErrors` int(11) DEFAULT NULL,
  `destinationScore` int(11) DEFAULT NULL,
  `dualnBackPoints` int(11) DEFAULT NULL,
  `dualnBackErrors` int(11) DEFAULT NULL,
  `dualnBackScore` int(11) DEFAULT NULL,
  `feedingPoints` int(11) DEFAULT NULL,
  `feedingErrors` int(11) DEFAULT NULL,
  `feedingScore` int(11) DEFAULT NULL,
  `swipePoints` int(11) DEFAULT NULL,
  `swipeErrors` int(11) DEFAULT NULL,
  `swipeScore` int(11) DEFAULT NULL,
  `matrixPoints` int(11) DEFAULT NULL,
  `matrixErrors` int(11) DEFAULT NULL,
  `matrixScore` int(11) DEFAULT NULL,
  `sequencePoints` int(11) DEFAULT NULL,
  `sequenceErrors` int(11) DEFAULT NULL,
  `sequenceScore` int(11) DEFAULT NULL,
  `shadowPoints` int(11) DEFAULT NULL,
  `shadowErrors` int(11) DEFAULT NULL,
  `shadowScore` int(11) DEFAULT NULL,
  `textColorPoints` int(11) DEFAULT NULL,
  `textColorErrors` int(11) DEFAULT NULL,
  `textColorScore` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `games`
--
-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `id` int(10) NOT NULL,
  `username` varchar(16) NOT NULL,
  `hash` varchar(100) NOT NULL,
  `age` int(3) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `players`
--

--
-- Indexes for dumped tables
--

--
-- Indexes for table `games`
--
ALTER TABLE `games`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `games`
--
ALTER TABLE `games`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `players`
--
ALTER TABLE `players`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
