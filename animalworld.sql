-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th4 26, 2024 lúc 11:42 AM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `animalworld`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `item_info`
--

CREATE TABLE `item_info` (
  `id` int(11) NOT NULL,
  `item` varchar(45) NOT NULL,
  `direction` varchar(135) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='item information';

--
-- Đang đổ dữ liệu cho bảng `item_info`
--

INSERT INTO `item_info` (`id`, `item`, `direction`) VALUES
(1, '1', 'ban co the xoa 1 hang'),
(2, '2', 'ban co the xoa 1 cot'),
(3, '3', 'ban co the xoa 1 hang 1 cot'),
(4, '4', 'ban co the xoa 1 hang 2 cot');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `pc_info`
--

CREATE TABLE `pc_info` (
  `id` int(11) NOT NULL,
  `idpc` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `pc_info`
--

INSERT INTO `pc_info` (`id`, `idpc`) VALUES
(1, 10102001),
(2, 9092018);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `play_score_table`
--

CREATE TABLE `play_score_table` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `idpc` int(11) NOT NULL,
  `item_1` int(11) NOT NULL,
  `item_2` int(11) NOT NULL,
  `item_3` int(11) NOT NULL,
  `item_4` int(11) NOT NULL,
  `score` int(11) NOT NULL,
  `money` int(11) NOT NULL,
  `level` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `play_score_table`
--

INSERT INTO `play_score_table` (`id`, `name`, `idpc`, `item_1`, `item_2`, `item_3`, `item_4`, `score`, `money`, `level`) VALUES
(1, 'admin', 10102001, 1, 1, 1, 1, 120, 200, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `store_market`
--

CREATE TABLE `store_market` (
  `id` int(11) NOT NULL,
  `item` int(11) NOT NULL,
  `cost` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='maket';

--
-- Đang đổ dữ liệu cho bảng `store_market`
--

INSERT INTO `store_market` (`id`, `item`, `cost`) VALUES
(1, 1, 10),
(3, 2, 10),
(4, 3, 10),
(5, 4, 10),
(7, 9, 10);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `item_info`
--
ALTER TABLE `item_info`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `pc_info`
--
ALTER TABLE `pc_info`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `play_score_table`
--
ALTER TABLE `play_score_table`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `store_market`
--
ALTER TABLE `store_market`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `item_info`
--
ALTER TABLE `item_info`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `pc_info`
--
ALTER TABLE `pc_info`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `play_score_table`
--
ALTER TABLE `play_score_table`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT cho bảng `store_market`
--
ALTER TABLE `store_market`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
